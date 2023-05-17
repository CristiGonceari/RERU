import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { Location } from '@angular/common';
import { ArticlesService } from '../../../utils/services/articles/articles.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ReferenceService } from '../../../utils/services/reference.service';

@Component({
  selector: 'app-faq-add-edit',
  templateUrl: './faq-add-edit.component.html',
  styleUrls: ['./faq-add-edit.component.scss']
})
export class FaqAddEditComponent implements OnInit {
  editorData: string = '';
  title: string;
  articleId;
  isLoading: boolean = true;
  isButtonPressed: boolean = false;
  public Editor = DecoupledEditor;
  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  title1: string;
  description: string;
  no: string;
  yes: string;

  articleForm: FormGroup;
  fileId: string;
  attachedFile: File;
  fileType: string = null;
  disableBtn: boolean = false;
  tags: any[] = [];
  placeHolder = '+ Rol';
  items = [];
  mobileButtonLength: string = "100%";

  constructor(
    private fb: FormBuilder,
    private articleService: ArticlesService,
    private activatedRoute: ActivatedRoute,
	  public translate: I18nService,
    private location: Location,
		private notificationService: NotificationsService,
    private referenceService: ReferenceService
  ) { }

  ngOnInit(): void {
    this.articleForm = new FormGroup({
			name: new FormControl()
		});
    this.onTextChange();
    this.activatedRoute.params.subscribe(params => {
      if (params.id){
        this.articleId = params.id;
        this.getArticle();
      } else {
				this.initForm();
				this.isLoading = false;
			}
    });
  }

  getArticle(): void {
    this.articleService.get(this.articleId).subscribe((res) => {
      if (res && res.data) {
        this.title = res.data.name;
        this.editorData = res.data.content;
        this.fileId = res.data.mediaFileId;

        res.data.roles.forEach(element => {
          this.tags.push({ display: element.label, value: +element.value })
        });

        this.initForm(res.data);
        this.isLoading = false;
      }
    })
  }

  initForm(data?): void {
		if (data) {
			this.articleForm = this.fb.group({
				name: this.fb.control(data?.name || null, [Validators.required])
			});
		} else {
			this.articleForm = this.fb.group({
				name: this.fb.control(null, [Validators.required])
			});
		}
	}

  checkConfirmButton(article: string, form){
    if (article !== '' && !form.invalid){
      return false;
    }
    return true;
  }

  onTextChange() {
		this.referenceService.getArticleRoles().subscribe(res => {
			res.data.forEach(element => {
				this.items.push({ display: element.label, value: +element.value })
			});
		})
	};

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  addArticle(){
    this.disableBtn = true;
    const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
    const request = new FormData();

    if (this.attachedFile) {
      this.fileType = '4';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
    request.append('Name', this.articleForm.value.name);
    request.append('Content', this.editorData);

    for (let i = 0; i < tagsArr.length; i++) {
      if (tagsArr[i].display !== '' && tagsArr[i].value !== '') {
        request.append('Roles[' + i + '][display]', tagsArr[i].display);
        request.append('Roles[' + i + '][value]', tagsArr[i].value);
      }
    }

    this.articleService.create(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('faq.succes-add-msg'),
        ]).subscribe(([title1, description]) => {
        this.title1 = title1;
        this.description = description;
        });
      this.backClicked();
      this.disableBtn = false;
      this.notificationService.success(this.title1, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  editArticle(){
    this.disableBtn = true;
    const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
    const request = new FormData();
    
    if (this.attachedFile) {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
    request.append('Data.Id', this.articleId);
    request.append('Data.Name', this.articleForm.value.name);
    request.append('Data.Content', this.editorData);

    if(this.fileId == "null" || this.fileId == null) this.fileId = null;
    request.append('Data.MediaFileId', this.fileId);

    for (let i = 0; i < tagsArr.length; i++) {
      if (tagsArr[i].display !== '' && tagsArr[i].value !== '') {
        request.append('Data.Roles[' + i + '][display]', tagsArr[i].display);
        request.append('Data.Roles[' + i + '][value]', tagsArr[i].value);
      }
    }

    this.articleService.edit(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('faq.succes-edit-msg'),
        ]).subscribe(([title1, description]) => {
        this.title1 = title1;
        this.description = description;
        });
      this.disableBtn = false;
      this.backClicked();
      this.notificationService.success(this.title1, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  saveArticle(): void {
    this.isButtonPressed = true;
    this.isLoading = true;
    if (this.articleId) {
      this.editArticle();
    } else {
      this.addArticle();
    }
  }

  backClicked() {
		this.location.back();
	}
}
