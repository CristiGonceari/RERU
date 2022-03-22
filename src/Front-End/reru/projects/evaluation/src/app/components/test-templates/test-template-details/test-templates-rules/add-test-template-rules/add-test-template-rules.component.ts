import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { RulesModel } from 'projects/evaluation/src/app/utils/models/test-templates/rules.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-add-test-template-rules',
  templateUrl: './add-test-template-rules.component.html',
  styleUrls: ['./add-test-template-rules.component.scss']
})
export class AddTestTemplateRulesComponent implements OnInit{
  editorData: string = '';
  id: number;
  public Editor = DecoupledEditor;
  isLoading: boolean = false;
  
  title: string;
  description: string;

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  constructor(private service: TestTemplateService,
    private activatedRoute: ActivatedRoute,
	  public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.params.subscribe(params => {
      this.id = params.id;
    });
    if (this.id) {
      this.get();
    }
  }

  get() {
    this.service.getRules(this.id).subscribe(res => {
      if (res && res.data) {
        this.editorData = res.data.rules;
        this.isLoading = false;
      }
    })
  }

  parse() {
    return {
      data: new RulesModel({
        testTemplateId: this.id,
        rules: this.editorData
      })
    }
  }

  save() {
    this.service.addRules(this.parse()).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-add-rules-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
		this.location.back();
	}
}
