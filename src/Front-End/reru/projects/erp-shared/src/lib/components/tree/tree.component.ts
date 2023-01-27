import { CollectionViewer, SelectionChange, DataSource } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BehaviorSubject, forkJoin, merge, Observable, of, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SelectionModel } from '@angular/cdk/collections';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { OrganigramService } from '../../services/organigram.service';
import { WarningAlertModalComponent } from '../../modals/warning-alert-modal/warning-alert-modal.component';

/** Flat node with expandable and level information */
export class DynamicFlatNode {
  constructor(public item: any, 
              public level = 1, 
              public expandable = false,
              public isLoading = false) {}
}
/**
 * File database, it can build a tree structured Json object from string.
 * Each node in Json object represents a file or a directory. For a file, it has filename and type.
 * For a directory, it has filename and children (a list of files or directories).
 * The input will be a json object string, and the output is a list of `FileNode` with nested
 * structure.
 */
export class DynamicDataSource implements DataSource<DynamicFlatNode> {
  rootLevelNodes: DynamicFlatNode[];
  dataChange = new BehaviorSubject<DynamicFlatNode[]>([]);
  checkSubject: Subject<DynamicFlatNode[]> = new Subject<DynamicFlatNode[]>();

  get data(): DynamicFlatNode[] { return this.dataChange.value; }
  set data(value: DynamicFlatNode[]) {
    this._treeControl.dataNodes = value;
    this.dataChange.next(value);
  }

  constructor(private readonly _treeControl: FlatTreeControl<DynamicFlatNode>,
              private readonly organigramService: OrganigramService,
              private readonly attachedUsers: number[]) {
                this.initialize();
              }

  initialize(): void {
    this.organigramService.getHead().subscribe(response => {
      this.data = [new DynamicFlatNode(response.data, 0, true)];
      this.rootLevelNodes = [new DynamicFlatNode(response.data, 0, true)];
    });
  }

  connect(collectionViewer: CollectionViewer): Observable<DynamicFlatNode[]> {
    this._treeControl.expansionModel.changed.subscribe(change => {
      if ((change as SelectionChange<DynamicFlatNode>).added ||
        (change as SelectionChange<DynamicFlatNode>).removed) {
        this.handleTreeControl(change as SelectionChange<DynamicFlatNode>);
      }
    });

    return merge(collectionViewer.viewChange, this.dataChange).pipe(map(() => this.data));
  }

  disconnect(collectionViewer: CollectionViewer): void {}

  /** Handle expand/collapse behaviors */
  handleTreeControl(change: SelectionChange<DynamicFlatNode>) {
    if (change.added) {
      change.added.forEach(node => this.toggleNode(node, true));
    }
    if (change.removed) {
      change.removed.slice().reverse().forEach(node => this.toggleNode(node, false));
    }
  }

  /**
   * Toggle the node, remove from display list
   */
  toggleNode(node: DynamicFlatNode, expand: boolean) {
    const rootNode = this.data[0].item;
    const isHead = !!node?.item?.organigramHeadName
    const index = this.data.indexOf(node);
    // If no children, or cannot find the node, no op
    if (index < 0) { 
      return;
    }

    node.isLoading = true;
    let contentParams;
    if (isHead) {
      contentParams = {
        ParentDepartmentId: node.item.parentDepartmentId,
        Type: node.item.type,
        OrganigramId: node.item.organigramId,
      }
    } else {
      contentParams = {
        ParentDepartmentId: node.item.id,
        Type: node.item.type,
        OrganigramId: rootNode.organigramId,
      }
    }

    const userParams = {
      Id: node.item.organigramId || node.item.id,
      Type: node.item.type,
    }

    if (expand) {
      forkJoin([
        this.organigramService.getContent(contentParams).pipe(catchError(() => of({data: []}))),
        userParams.Type ? this.organigramService.getUsers(userParams).pipe(catchError(() => of({ data: []}))) : of(true),
      ]).subscribe(([contentResponse, usersResponse]) => {
      const departments = contentResponse.data;
      const users = usersResponse.data;
      const departmentNodes = departments.map((item) => new DynamicFlatNode(item, node.level + 1, isHead));
      const userNodes = users.map((item) => new DynamicFlatNode(item, node.level + 1, isHead));
      this.data.splice(index + 1, 0, ...userNodes, ...departmentNodes);

      // notify the change
      this.dataChange.next(this.data);

      // check previously checked users from table list
      const checkedUsers = userNodes.map((node: DynamicFlatNode) => {
        if (this.attachedUsers.includes(node.item.id)) {
          return node;
        }
      }).filter((el: DynamicFlatNode) => !!el);
      this.checkSubject.next(checkedUsers);

      node.isLoading = false;
      })
    } else {
      let count = 0;
      for (let i = index + 1; i < this.data.length
        && this.data[i].level > node.level; i++, count++) {}
      this.data.splice(index + 1, count);
      // notify the change
      this.dataChange.next(this.data);
      node.isLoading = false;
    }

  }
}

/**
 * @title Tree with dynamic data
 */
@Component({
  selector: 'erp-shared-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.scss']
})
export class TreeComponent {
  @Output() changeAttachedUser: EventEmitter<number[]> = new EventEmitter<number[]>();
  @Input() attachedUsers: number[] = [];
  isLoading: boolean = true;
  constructor(readonly organigramService: OrganigramService,
              private readonly modalService: NgbModal) {
    this.treeControl = new FlatTreeControl<DynamicFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new DynamicDataSource(this.treeControl, organigramService, this.attachedUsers);
    this.subscribeForCheckChanges();
  }

  treeControl: FlatTreeControl<DynamicFlatNode>;

  dataSource: DynamicDataSource;

  checklistSelection = new SelectionModel<any>(true /* multiple */);

  getLevel = (node: DynamicFlatNode) => node.level;

  isExpandable = (node: DynamicFlatNode) => node.expandable;

  hasChild = (_: number, _nodeData: DynamicFlatNode) => _nodeData.expandable;

  hasUsers = (nodes: DynamicFlatNode[]) => nodes.some((node: DynamicFlatNode) => !!node.item.firstName);

  subscribeForCheckChanges(): void {
    this.dataSource.checkSubject.subscribe((response: DynamicFlatNode[]) => {
      response.forEach((node: DynamicFlatNode) => this.todoLeafItemSelectionToggle(node, null));
      const checkedUserIds = response.map((node: DynamicFlatNode) => node.item.id);
      this.changeAttachedUser.emit([...this.attachedUsers, ...checkedUserIds]);
    });
  }

  /** Whether all the descendants of the node are selected. */
  descendantsAllSelected(node: DynamicFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.length > 0 && descendants.every(child => {
      return this.checklistSelection.isSelected(child);
    });
    return descAllSelected;
  }

  openWarningModal(): void {
    this.modalService.open(WarningAlertModalComponent, { centered: true });
  }

  /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
  todoLeafItemSelectionToggle(node: DynamicFlatNode, event): void {
    if (node.item.relationId) {
      this.todoItemSelectionToggle(event, node);
    } else {
      this.checklistSelection.toggle(node);
      this.checkAllParentsSelection(node);
    }
  }

  /** Toggle the to-do item selection. Select/deselect all the descendants node */
  todoItemSelectionToggle(node: DynamicFlatNode, event): void {
    const descendants = this.treeControl.getDescendants(node instanceof DynamicFlatNode ? node : event);
    if (!this.hasUsers(descendants)) {
      this.openWarningModal();
      if (!event.source) {
        (node as any).source.checked = false;
      } else {
        event.source.checked = false;
      }
      return;
    }
    this.checklistSelection.toggle(node instanceof DynamicFlatNode ? node : event);
    this.checklistSelection.isSelected(node instanceof DynamicFlatNode ? node : event)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    // Force update for the parent
    descendants.forEach(child => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node instanceof DynamicFlatNode ? node : event);
  }

  /* Checks all the parents when a leaf node is selected/unselected */
  checkAllParentsSelection(node: DynamicFlatNode): void {
    let parent: DynamicFlatNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  /* Get the parent node of a node */
  getParentNode(node: DynamicFlatNode): DynamicFlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  /** Check root node checked state and change it accordingly */
  checkRootNodeSelection(node: DynamicFlatNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.length > 0 && descendants.every(child => {
      return this.checklistSelection.isSelected(child);
    });
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }
}
