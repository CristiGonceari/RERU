import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { NotificationsService } from 'angular2-notifications';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { Location } from '@angular/common';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { RulesModel } from 'projects/evaluation/src/app/utils/models/test-types/rules.model';

@Component({
  selector: 'app-add-test-type-rules',
  templateUrl: './add-test-type-rules.component.html',
  styleUrls: ['./add-test-type-rules.component.scss']
})
export class AddTestTypeRulesComponent implements OnInit{
  editorData: string = '';
  id: number;
  public Editor = DecoupledEditor;
  isLoading: boolean = false;

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  constructor(private service: TestTypeService,
    private activatedRoute: ActivatedRoute,
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
        testTypeId: this.id,
        rules: this.editorData
      })
    }
  }

  save() {
    this.service.addRules(this.parse()).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Rules were successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
		this.location.back();
	}
}
