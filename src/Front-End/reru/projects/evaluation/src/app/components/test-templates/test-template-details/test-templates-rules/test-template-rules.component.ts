import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-test-template-rules',
  templateUrl: './test-template-rules.component.html',
  styleUrls: ['./test-template-rules.component.scss']
})
export class TestTemplatesRulesComponent implements OnInit{
  testTemplateId
  editorData: string = '';
  status: string;
  add = false;
  public Editor = DecoupledEditor;
  isLoading: boolean = false;

  constructor(private route: ActivatedRoute, private service: TestTemplateService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {this.testTemplateId = params.id;
      if (this.testTemplateId) {
        this.get();
        this.getTest();
      }
    });
  }

  get() {
    this.service.getRules(this.testTemplateId).subscribe(res => {
      if(res && res.data) {
        this.editorData = res.data.rules;
        this.isLoading = false;
      }
      if(res.data.rules == null) this.add = true;
      }
    );
  }

  getTest(){
    this.service.getTestTemplate(this.testTemplateId).subscribe(res => this.status = res.data.status);
  }
}
