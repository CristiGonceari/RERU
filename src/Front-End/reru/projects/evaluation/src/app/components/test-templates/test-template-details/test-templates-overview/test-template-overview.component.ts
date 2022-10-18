import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestTemplateModeEnum } from 'projects/evaluation/src/app/utils/enums/test-template-mode.enum';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { QualifyingTypeEnum } from 'projects/evaluation/src/app/utils/enums/qualifying-type.enum';
import { TestTemplate } from 'projects/evaluation/src/app/utils/models/test-templates/test-template.model';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-template-overview',
  templateUrl: './test-template-overview.component.html',
  styleUrls: ['./test-template-overview.component.scss']
})
export class TestTemplateOverviewComponent implements OnInit {
  testId: number;
  status;
  qualifyingType;
  testTemplate: TestTemplate;
  testEnum = TestTemplateModeEnum;
  isLoading: boolean = false;

  constructor(
    private service: TestTemplateService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      this.testId = params.id;
      this.getTestTemplate();
		});
  }

  getTestTemplate(){
    this.service.getTestTemplate(this.testId).subscribe(
      (res) => {
        if(res && res.data) {
          this.testTemplate = res.data;
          this.isLoading = false;
          this.status = TestTemplateStatusEnum[res.data.status];
          this.qualifyingType = QualifyingTypeEnum[res.data.qualifyingType];
        }
    })
  }

  backClicked() {
		this.location.back();
	}
}
