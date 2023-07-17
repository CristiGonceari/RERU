/*import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { MyPollStatusEnum } from '../../../../utils/enums/my-poll-status.enum';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../../utils/services/i18n/i18n.service';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
 
@Component({
  selector: 'app-polls-table',
  templateUrl: './polls-table.component.html',
  styleUrls: ['../../table-inherited.component.scss']
})
export class PollsTableComponent implements OnInit {
  polls = [];
  @Input() id: number;
  isLoadingCountedTests: boolean = true;
  isLoading: boolean = true;
  enum = MyPollStatusEnum;
  testEnum = TestStatusEnum;
  date: any;
  countedTests: Events[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  selectedDay;
  displayDate;

  downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
  filters: any = {};

  constructor(private testService: TestService, 
    private router: Router, 
    private route: ActivatedRoute,
    private modalService: NgbModal,
	  public translate: I18nService,
    ) { }

  ngOnInit(): void {
  }

  
  getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByName('column');
		let headersDto = [
      'testTemplateName', 
      'eventName',
      'testStatus', 
      'votedTime', 
      'startTime',
      'endTime', 
    ];
    
		for (let i = 0; i < headersHtml.length - 8; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
    
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
      date: this.selectedDay,
      ...this.filters
		};
    
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      this.translate.get('print.select-file-format'),
			this.translate.get('print.max-print-rows')
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
    
		this.testService.printMyPolls(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	} 

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date);
    }

    const request = {
      date: this.selectedDay,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getMyPolls(request).subscribe(response => {
      if (response.success) {
        this.polls = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  getListOfCoutedTests(data) {
    this.isLoadingCountedTests = true;

    const request = {
      startTime: this.parseDates(data.fromDate),
      endTime: this.parseDates(data.tillDate)
    }

    this.testService.getMyPollsCount(request).subscribe(
      response => {
        if (response.success) {
          this.countedTests = response.data;

          for (let calendar of data.calendar) {
            let data = new Date(calendar.date);

            for (let values of response.data) {
              let c = new Date(values.date);
              let compararea = +data == +c;

              if (compararea) {
                calendar.count = values.count;
              }
            }
          }
        }
        this.isLoadingCountedTests = false;
      }, () => {
        this.isLoadingCountedTests = false;
      }
    )
  }

  parseDates(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;
  }

  parseDatesForTable(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${dayWithZero}/${monthWithZero}/${year}`;
  }

  createPoll(check: boolean, testTemplateId?, eventId?) {
    this.testService.createMinePoll({testTemplateId: testTemplateId, eventId: eventId}).subscribe((res) => {
      if(check)
        this.router.navigate(['../../polls/start-poll-page', res.data], { relativeTo: this.route });
      else 
       this.router.navigate(['../../polls/performing-poll', res.data], { relativeTo: this.route });
    });
  }
}
*/