import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyPollStatusEnum } from 'projects/evaluation/src/app/utils/enums/my-poll-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-polls-by-event',
  templateUrl: './polls-by-event.component.html',
  styleUrls: ['./polls-by-event.component.scss']
})
export class PollsByEventComponent implements OnInit {
  polls = [];
  @Input() id: number;
  isLoading: boolean = true;
  pagedSummary: PaginationModel = new PaginationModel();
  enum = MyPollStatusEnum;
  testEnum = TestStatusEnum;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];
  date;
  userId;

  constructor(
    private testService: TestService, 
    private activatedRoute: ActivatedRoute,
    public translate: I18nService,
	  private modalService: NgbModal
    ) { }

  ngOnInit(): void {
    this.date = new Date().toISOString();
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      if (params.id && this.id) {
        this.userId = params.id;
        this.getPolls();
      }
    });
  }

  getPolls(data: any = {}){
    const params: any = {
      eventId: this.id,
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUsersPollsByEvent(params).subscribe(
      (res) => {
          this.polls = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
      }
    )
  }

  getHeaders(name: string): void {
		this.translateData();
		let pollsTable = document.getElementById('pollsTable')
		let headersHtml = pollsTable.getElementsByTagName('th');
		let headersDto = ['testTemplateName', 'testStatus', 'votedTime','startTime', 'endTime'];
		for (let i=0; i<headersHtml.length; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			userId: this.userId,
      eventId: this.id,
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
		this.testService.printUserPolls(data).subscribe(response => {
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
