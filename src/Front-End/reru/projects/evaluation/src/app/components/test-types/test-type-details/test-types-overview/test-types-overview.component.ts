import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestTypeModeEnum } from 'projects/evaluation/src/app/utils/enums/test-type-mode.enum';
import { TestTypeStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-type-status.enum';
import { TestType } from 'projects/evaluation/src/app/utils/models/test-types/test-type.model';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-types-overview',
  templateUrl: './test-types-overview.component.html',
  styleUrls: ['./test-types-overview.component.scss']
})
export class TestTypesOverviewComponent implements OnInit {
  testId: number;
  status;
  testType: TestType;
  testEnum = TestTypeModeEnum;
  isLoading: boolean = false;

  constructor(
    private service: TestTypeService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      this.testId = params.id;
      this.getTestType();
		});
  }

  getTestType(){
    this.service.getTestType(this.testId).subscribe(
      (res) => {
        if(res && res.data) {
          this.testType = res.data;
          this.isLoading = false;
          this.status = TestTypeStatusEnum[res.data.status];
        }
        console.log("testType", this.testType);
        console.log("status", this.status);
        
    })
  }

  backClicked() {
		this.location.back();
	}
}
