import { Component, OnInit } from '@angular/core';
import { StatisticsQuestionFilterEnum } from '../../../utils/enums/statistics-question-filter.enum';
import { StatisticService } from '../../../utils/services/statistic/statistic.service';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';


@Component({
  selector: 'app-statisctics-table',
  templateUrl: './statisctics-table.component.html',
  styleUrls: ['./statisctics-table.component.scss']
})
export class StatiscticsTableComponent implements OnInit {

  questionList = [];
  data;
  printData;
  enum = StatisticsQuestionFilterEnum;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

  constructor(
    private statisticService: StatisticService,
		private modalService: NgbModal,
		public translate: I18nService,
    ) { }

  ngOnInit(): void {
  }

  getQuestions(data) {
    this.data = data;
    if (data.categoryId)
      this.statisticService.getCategoryQuestions(data).subscribe((res) => { this.questionList = res.data });
    else if (data.testTemplateId)
      this.statisticService.getTestTemplateQuestions(data).subscribe((res) => { this.questionList = res.data });
  }

  getHeaders(name: string): void {
		this.translateData();
    let headersHtml = document.getElementsByTagName('th');
    let headersDto = ['question', 'categoryName', 'totalUsed', 'percent'];
    for (let i=0; i<headersHtml.length; i++) {
      this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
    }
    if (this.data.categoryId) {
      this.printData = {
        tableName: name,
        fields: this.headersToPrint,
        orientation: 2,
        categoryId: +this.data.categoryId,
        filterEnum: +this.data.filterEnum,
        itemsPerPage: +this.data.itemsPerPage
      };
    } else if (this.data.testTemplateId) {
      this.printData = {
        tableName: name,
        fields: this.headersToPrint,
        orientation: 2,
        testTemplateId: +this.data.testTemplateId,
        filterEnum: +this.data.filterEnum,
        itemsPerPage: +this.data.itemsPerPage
      };
    }
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = this.printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel'];
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
    if(this.data.categoryId) {
      this.statisticService.printByCategory(data).subscribe(response => {
        if (response) {
          const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], data.tableName, { type: response.body.type });
          saveAs(file);
          this.downloadFile = false;
        }
      }, () => this.downloadFile = false);
    } else if (this.data.testTemplateId) {
      this.statisticService.printByTestTemplate(data).subscribe(response => {
        if (response) {
          const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], data.tableName, { type: response.body.type });
          saveAs(file);
          this.downloadFile = false;
        }
      }, () => this.downloadFile = false);
    }
	}
}
