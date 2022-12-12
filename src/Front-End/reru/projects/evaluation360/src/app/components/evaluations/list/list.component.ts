import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { EvaluationsTableComponent } from '../evaluations-table/evaluations-table.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements AfterViewInit {
  @ViewChild(EvaluationsTableComponent) evaluationsTable: EvaluationsTableComponent;
  @ViewChild('highlight') highlight: ElementRef;
  private readonly highlightKey = 'HIGHLIGHT';
  includeAll: boolean;
  constructor(private readonly cd: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.initHighlight();
    this.cd.detectChanges();
  }

  initHighlight(): void {
    const highlight = localStorage.getItem(this.highlightKey);
    if (highlight && highlight === 'true') this.highlight.nativeElement.checked = true;
  }

  filterList(includeAll: boolean): void {
    if (includeAll) {
      this.evaluationsTable.includeAll = true;
      this.evaluationsTable.list();
    } else {
      this.evaluationsTable.includeAll = false;
      this.evaluationsTable.list();
    }
  }

  handleHighlightChange(value: boolean): void {
    if (value) {
      localStorage.setItem(this.highlightKey, value+'');
      return;
    }

    localStorage.removeItem(this.highlightKey);
  }
}
