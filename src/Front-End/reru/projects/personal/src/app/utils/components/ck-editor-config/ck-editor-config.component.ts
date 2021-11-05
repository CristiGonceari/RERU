import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgbPanelChangeEvent, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { PagedSummary } from '../../models/paged-summary.model';
import { ObjectUtil } from '../../util/object.util';
import { saveAs } from 'file-saver';
import { DocumentsTemplateService } from '../../services/documents-template.service';
import { FileService } from '../../services/file.service';



@Component({
  selector: 'app-ck-editor-config',
  templateUrl: './ck-editor-config.component.html',
  styleUrls: ['./ck-editor-config.component.scss']
})
export class CkEditorConfigComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input('selectedType') selectedType = 0;
  @Output() editedCkeditorValue :EventEmitter<string> = new EventEmitter<string>();

  isLoading: boolean = true;
  documents: any[] = [];

  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 0,
    totalPages: 0
  };

  documentId:number = 3
  model: any;
  editorValue: string = '';
  selectedDocument: number = 0;

  constructor(
    private documentService: DocumentsTemplateService,
    private documentTemplateService: DocumentsTemplateService,
    private fileService: FileService
     ) { }

  ngOnInit(): void {
    this.getList();
  }
  changeValue($event){
    this.editedCkeditorValue.emit(this.editorValue)
  }

  retrieveDocuments(): void {
    this.documentTemplateService.getById(this.selectedDocument).subscribe((res) => {
      if (res && res.data) {
        this.editorValue = res.data.value;
        this.editedCkeditorValue.emit(this.editorValue)
        this.isLoading = false;
      }
    })
  }
  
  beforeChange($event: NgbPanelChangeEvent) {
    if ($event.panelId === 'preventchange-2') {
      $event.preventDefault();
    }

    if ($event.panelId === 'preventchange-3' && $event.nextState === false) {
      $event.preventDefault();
    }
  }

  getList(data :any = {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize
    })
    this.documentService.list(request).subscribe(response => {
      if (response.success) {
        this.documents = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  ckEditorContentToPdf(): void {
    this.isLoading = true;
    
    this.fileService.getPdfFromString(this.editorValue).subscribe(response => {
    
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
      
      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
      this.isLoading = false;
    });
  }


}
