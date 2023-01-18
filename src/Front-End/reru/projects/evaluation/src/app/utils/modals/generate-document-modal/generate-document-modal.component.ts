import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../models/pagination.model';
import { DocumentTemplateService } from '../../services/document-template/document-template.service';
import { TestTemplateService } from '../../services/test-template/test-template.service';
import { ObjectUtil } from '../../util/object.util';
import { saveAs } from 'file-saver';
import { DocumentTemplate } from '../../models/document-template/document-templates.model';
import { TestService } from '../../services/test/test.service';
import { CKEditorModule } from 'ngx-ckeditor';
import { map } from 'rxjs/operators'
@Component({
  selector: 'app-generate-document-modal',
  templateUrl: './generate-document-modal.component.html',
  styleUrls: ['./generate-document-modal.component.scss'],
})
export class GenerateDocumentModalComponent implements OnInit {

  @Input() id: number;
  @Input() testName: string;
  @Input() fileType: any;
  @Input('selectedType') selectedType = 0;
  @Output() editedCkeditorValue :EventEmitter<string> = new EventEmitter<string>();
  
  isLoading: boolean = true;
  isSelectedDocument: boolean = false;
  documents: DocumentTemplate[] = [];

  pagination: PaginationModel = new PaginationModel();

  documetFileTypes: any;
  model: any;
  editorValue: any;
  selectedDocument: number;
  editor: string[] =[];
  
  value: number;
  documentEditedValue: string;

  constructor(
    private documentService: DocumentTemplateService,
    private testTemplate: TestTemplateService,
    private testService: TestService,
    private activeModal: NgbActiveModal,
  ) {}

  ngOnInit(): void {
    this.getList();
  }

  changeValue(event) {
    this.editedCkeditorValue.emit(this.editorValue)
  }

  retrieveDocuments(evt): void {
    this.isLoading = true;
    this.selectedDocument = evt;
    
    if(evt == 0) {
      this.isSelectedDocument = false;
      return;
    }
    else {
    this.isSelectedDocument = true;
    }

    if(this.fileType == 2){
      const request= ObjectUtil.preParseObject({
        testTemplateId: this.id,
        documentTemplateId: this.selectedDocument
      })
      this.testTemplate.getTestTemplateDocumentReplacedKeys(request).subscribe((res) => {
        if (res && res.data) {
          this.editorValue = res.data;
          this.editedCkeditorValue.emit(this.editorValue)
          this.isLoading = false;
        }
      })
    } else {
      const request= ObjectUtil.preParseObject({
        testId: this.id,
        documentTemplateId: this.selectedDocument
      })
      this.testService.getTestDocumentReplacedKeys(request).subscribe((res) => {
        if (res && res.data) {
          this.editorValue = res.data;
          this.editedCkeditorValue.emit(this.editorValue)
          this.isLoading = false;
        }
      })
    }
  }

  getList(data :any = {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      fileType: this.fileType || '',
      page: data.page || this.pagination.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagination.pageSize
    })
    this.documentService.list(request).subscribe(response => {
      if (response.success) {
        let mappedItems = response.data.items.map((item) => {
          return {Label: item.name, Value: item.id}
        })

        this.documents = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  ckEditorContentToPdf(): void {
    if(this.fileType == 1){
      const request= ObjectUtil.preParseObject({
        source: this.editorValue,
        testTemplateName: this.testName
      })
      this.testTemplate.printDocument(request).subscribe(response => {
        const fileName = this.testName;
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
        this.isLoading = false; 
        this.close();
      });
    } else {
      const request= ObjectUtil.preParseObject({
        source: this.editorValue,
        testName: this.testName
      })
      this.testService.printDocument(request).subscribe(response => {
        const fileName = this.testName;
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
        this.isLoading = false; 
        this.close();
      });
    }
  }
  
  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
