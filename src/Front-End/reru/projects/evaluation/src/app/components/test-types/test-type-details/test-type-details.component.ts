import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTypeStatusEnum } from '../../../utils/enums/test-type-status.enum';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-type-details',
  templateUrl: './test-type-details.component.html',
  styleUrls: ['./test-type-details.component.scss']
})
export class TestTypeDetailsComponent implements OnInit {
  testId: number;
  testName;
  status;
  mode;
  statusEnum = TestTypeStatusEnum;
  isLoading: boolean = false;

  constructor(
    private service: TestTypeService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
      this.get();
		});
  }

  get(){
    this.service.getTestType(this.testId).subscribe(
      (res) => {
        if(res && res.data) {
          this.isLoading = false;
          this.testName = res.data.name;
          this.mode = res.data.mode;
          this.status = res.data.status;
        }
    })
  }

  cloneTestType(id): void {
		this.service.clone(id).subscribe(res => {
			if (res && res.data) this.router.navigate(['../test-type']);
		});
	}

  backClicked() {
		this.location.back();
	}

  changeStatus() {
		let params;

		if (this.status == TestTypeStatusEnum.Draft)
			params = { testTypeId: this.testId, status: TestTypeStatusEnum.Active }
		else if (this.status == TestTypeStatusEnum.Active)
			params = { testTypeId: this.testId, status: TestTypeStatusEnum.Canceled }

		this.service.changeStatus({ input: params }).subscribe(() => {});
	}

	validateTestType() {
		this.service.validateTestType({testTypeId: this.testId}).subscribe(() => this.changeStatus());
	}


}
