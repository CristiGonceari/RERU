import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { saveAs } from 'file-saver';
import { Observable } from 'rxjs';
import { DocumentTemplateService } from '../../../utils/services/document-template/document-template.service';
import { DocumentKeyEnum } from '../../../utils/models/document-template/document-template-keys.enum';
import { DocumentTemplateKeys } from '../../../utils/models/document-template/document-template-keys.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { SelectItem } from '../../../utils/models/select-item.model';



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
  fileType = new SelectItem();
  forEditFileType: any;
  editor: string[] =[];
  wordButton: any;
  isAdded: boolean = false;
  public firebase_Data : Observable<DocumentTemplateKeys>;
  keysEnum = DocumentKeyEnum;

  constructor(
    private documentTemplateService: DocumentTemplateService,
    private referenceService: ReferenceService,
    private location: Location,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute,
    // private fileService: FileService ,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.documentId = params.id;
    });
    if (this.documentId) {
      this.getDocument();
      
    } else {
      this.isLoading = false;
    }
    this.getTemplateType();
  }
 
  addWordButtonToCkEditor(word){
    this.isAdded = true;
    this.wordButton = word;
    
  }

  demo()
  {
    console.log("value", this.forEditFileType);
      
  }
  public ckEditorFocusCursor(event) : void {
    var selection = event.editor.getSelection();
    var range = selection.getRanges()[0];
    var cursor_position = range.startOffset;
    if(this.isAdded){
      var add = event.editor.insertHtml(`<span>${this.wordButton}</span>`)
    }
    this.isAdded = false;
}

  getDocument(): void {
    this.documentTemplateService.getById(this.documentId).subscribe((res) => {
      if (res && res.data) {
        this.title = res.data.name;
        this.editorValue = res.data.value;
        this.forEditFileType = res.data.fileType;
        this.isLoading = false;
        this.getListOfKeys(res.data.fileType);
      }
    })
  }

  getTemplateType(){
    this.referenceService.getDocumentTemplateType().subscribe((res) => {
      this.fileType = res.data;
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
        this.notificationService.success('Success', 'Document was successfully updated', NotificationUtil.getDefaultMidConfig());
      });
    } else {
      this.documentTemplateService.create(createDocument).subscribe(() => {
        this.backCancel();
        this.notificationService.success('Success', 'Document was successfully added', NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  backCancel() {
    this.location.back();
  }

  // ckEditorContentToPdf(): void {
  //   this.isLoading = true;
    
  //   this.fileService.getPdfFromString(this.editorValue).subscribe(response => {
    
  //     let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
      
  //     if (response.body.type === 'application/pdf') {
  //       fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
  //     }

  //     const blob = new Blob([response.body], { type: response.body.type });
  //     const file = new File([blob], fileName, { type: response.body.type });
  //     saveAs(file);
  //     this.isLoading = false;
  //   });
  // }
}
