import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';
import { DocumentTemplateService } from '../../../utils/services/document-template/document-template.service';
import { DocumentKeyEnum } from '../../../utils/models/document-template/document-template-keys.enum';
import { DocumentTemplateKeys } from '../../../utils/models/document-template/document-template-keys.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { SelectItem } from '../../../utils/models/select-item.model';
import { saveAs } from 'file-saver';
import { ObjectUtil } from '../../../utils/util/object.util';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  isLoading: boolean = true;
  documentId: number;
  filterForm: FormGroup;
  editorValue: string = '';
  title: string;
  description: string;
  fileType = new SelectItem({value: "0", label: "Select"});
  forEditFileType: any = [];
  editor: string[] =[];
  wordButton: any;
  isAdded: boolean = false;
  public firebase_Data : Observable<DocumentTemplateKeys>;
  keysEnum = DocumentKeyEnum;

  constructor(
    private documentTemplateService: DocumentTemplateService,
    private referenceService: ReferenceService,
    private testTemplate: TestTemplateService,
    private location: Location,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;

    this.getTemplateType();

    this.activatedRoute.params.subscribe(params => {
      this.documentId = params.id;
    });
    if (this.documentId) {
      this.getDocument();
      
    } else {
    }
  }
  
 
  addWordButtonToCkEditor(word){
    this.isAdded = true;
    this.wordButton = word;
  }
  
  public ckEditorFocusCursor(event) : void {
    const selection = event.editor.getSelection();
    const range = selection.getRanges()[0];
    const cursor_position = range.startOffset;
    if(this.isAdded){
      const add = event.editor.insertHtml(`<span>${this.wordButton}</span>`)
    }
    this.isAdded = false;
}

  getDocument(): void {
    this.documentTemplateService.getById(this.documentId).subscribe((res) => {
      if (res && res.data) {
        this.title = res.data.name;
        this.editorValue = res.data.value;
        this.forEditFileType = res.data.fileType;
        this.getListOfKeys(res.data.fileType);
      }
    })
  }

  getTemplateType(){
    this.referenceService.getDocumentTemplateType().subscribe((res) => {
      this.fileType = res.data;
      this.isLoading = false;
    })
  }

  getListOfKeys(value): void {
    const testType = {
      fileType: value
    }

    this.documentTemplateService.getListOfKeys(testType).subscribe(res => {
       this.firebase_Data = res.data;
    })
  }
  
  saveDocument(): void {
    const createDocument = {
      name: this.title,
      value: this.editorValue,
      fileType: this.forEditFileType
    }
    const editDocument = {
      id: this.documentId,
      name: this.title,
      fileType: this.forEditFileType,
      value: this.editorValue
    }
    if (this.documentId) {
      this.documentTemplateService.edit(editDocument).subscribe(() => {
        this.backCancel();

        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('require-documents.document-edited-msg'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });

        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      });
    } else {
      this.documentTemplateService.create(createDocument).subscribe(() => {
        this.backCancel();

        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('require-documents.document-added-msg'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });

        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  ckEditorContentToPdf(): void {
      const request= ObjectUtil.preParseObject({
        source: this.editorValue,
        testTemplateName: this.title
      })
      this.testTemplate.printDocument(request).subscribe(response => {
        const fileName = this.title;
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
        this.isLoading = false; 
      });
  }

  backCancel() {
    this.location.back();
  }
}
