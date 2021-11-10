import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-test-types-rules',
  templateUrl: './test-types-rules.component.html',
  styleUrls: ['./test-types-rules.component.scss']
})
export class TestTypesRulesComponent implements OnInit{
  testTypeId
  editorData: string = '';
  status: string;
  add = false;
  public Editor = DecoupledEditor;
  isLoading: boolean = false;

  constructor(private route: ActivatedRoute, private service: TestTypeService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {this.testTypeId = params.id;
      if (this.testTypeId) {
        this.get();
        this.getTest();
      }
    });
  }

  get() {
    this.service.getRules(this.testTypeId).subscribe(res => {
      if(res && res.data) {
        this.editorData = res.data.rules;
        this.isLoading = false;
      }
      if(res.data.rules == null) this.add = true;
      }
    );
  }

  getTest(){
    this.service.getTestType(this.testTypeId).subscribe(res => this.status = res.data.status);
  }
}
