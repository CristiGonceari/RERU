import { Component, OnInit } from '@angular/core';
import * as DecoupledEditor  from '@ckeditor/ckeditor5-build-decoupled-document';
import { DocumentsTemplateService } from '../../../utils/services/documents-template.service';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { saveAs } from 'file-saver';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { CdkDragDrop, transferArrayItem } from '@angular/cdk/drag-drop';
import { Observable } from 'rxjs';
import { DocumentsTemplateListOfKeys } from '../../../utils/models/DocumentsTemplatesListOfKeys';
import { DocumentKeyEnum } from '../../../utils/models/documentKeys.enum';
import { CategoryKeyEnum } from '../../../utils/models/categoryKeyEnum';



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
  fileType: string;
  editor: string[] =[];
  wordButton: any;
  isAdded: boolean = false;
  public firebase_Data : Observable<DocumentsTemplateListOfKeys>;
  keysEnum = DocumentKeyEnum;
  categoryKeyEnum = CategoryKeyEnum;

  constructor(
    private documentTemplateService: DocumentsTemplateService,
    private location: Location,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute,
    private fileService: FileService ,
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
    this.getListOfKeys();
  }
 
  addWordButtonToCkEditor(word){
    this.isAdded = true;
    this.wordButton = word;
    
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
        this.fileType = res.data.fileType;
        this.isLoading = false;
      }
    })
  }

  getListOfKeys(): void {
    this.documentTemplateService.getListOfKeys().subscribe(res => {
       this.firebase_Data = res.data;
    })
  }
  
  saveDocument(): void {
    const createDocument = {
      name: this.title,
      value: this.editorValue,
      fileType: this.fileType
    }
    const editDocument = {
      id: this.documentId,
      name: this.title,
      fileType: this.fileType,
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
