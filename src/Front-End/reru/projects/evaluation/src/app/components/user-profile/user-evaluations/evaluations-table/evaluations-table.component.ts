import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { TestService } from '../../../../utils/services/test/test.service';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum'
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum'
import { saveAs } from 'file-saver';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-evaluations-table',
  templateUrl: './evaluations-table.component.html',
  styleUrls: ['./evaluations-table.component.scss']
})
export class EvaluationsTableComponent implements OnInit {
  testRowList: [] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  userId: number;
  isLoading: boolean = true;
  enum = TestStatusEnum;
  result = TestResultStatusEnum;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];

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
        this.getUsersEvaluations();
      }
    });
  }

  getUsersEvaluations(data: any = {}) {
    this.isLoading = true;
    const params: any = {
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUsersEvaluations(params).subscribe(
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
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = [
      'testTemplateName', 
      'userName',
      'eventName', 
      'testStatus', 
      'result'
    ];
    
		for (let i = 0; i < headersHtml.length; i++) {
      if(i == 2){
        this.headersToPrint.push({ value: "idnp", label: "Idnp",isChecked: true})
      }
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

    console.log(this.headersToPrint)
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
        for (let i = 0; i < this.printTranslates.length; i++) {
          this.printTranslates[i] = items[i];
        }
      }
    );
  }

  printTable(data): void {
    this.downloadFile = true;

    this.testService.printUserEvaluations(data).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
        this.downloadFile = false;
      }
    }, () => this.downloadFile = false);
  }
}
