import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintModalComponent } from '@erp/shared';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-user-evaluated-tests-list',
  templateUrl: './user-evaluated-tests-list.component.html',
  styleUrls: ['./user-evaluated-tests-list.component.scss']
})
export class UserEvaluatedTestsListComponent implements OnInit {

  testRowList: [] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  userId: number;
  isLoading: boolean = true;
  enum = TestStatusEnum;
  resultEnum = TestResultStatusEnum;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];
  title: string;
  
  constructor(
    private testService: TestService, 
    private activatedRoute: ActivatedRoute,
    public translate: I18nService,
	  private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      if (params.id) {
        this.userId = params.id;
        this.getUserTests();
      }
    });
  }

  getUserTests(data: any = {}) {
    const params: any = {
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUserEvaluatedTests(params).subscribe(
      res => {
        if (res && res.data) {
          this.testRowList = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
        }
      }
    )
  }

  getHeaders(name: string): void {
		this.translateData();
		let evaluatedTestTable = document.getElementById('evaluatedTestTable')
		let headersHtml = evaluatedTestTable.getElementsByTagName('th');
		let headersDto = ['programmedTime', 'testStatus', 'testTemplateName', 'accumulatedPercentage', 'result'];
		for (let i=0; i<headersHtml.length; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			userId: this.userId
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

  
	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.testService.printUserEvaluatedTests(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}