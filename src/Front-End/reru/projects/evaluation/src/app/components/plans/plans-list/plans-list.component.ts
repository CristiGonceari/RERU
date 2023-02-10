import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { EventCalendarComponent } from '../../../utils/components/event-calendar/event-calendar.component';
import { CalendarDay } from '../../../utils/models/calendar/calendarDay';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-plans-list',
  templateUrl: './plans-list.component.html',
  styleUrls: ['./plans-list.component.scss'],
})


export class PlansListComponent implements OnInit {

  @ViewChild(EventCalendarComponent) currentMonth: boolean;

  public calendar: CalendarDay[] = [];

  selectedDay;
  date: Date;

  isLoading: boolean = true;
	downloadFile: boolean = false;
  isLoadingCalendar: boolean = true;
  isLoadingCountedTests: boolean = true;

	headersToPrint = [];
	printTranslates: any[];

  plans: any[] = [];
  pagination: PaginationModel = new PaginationModel();
  eventuri: any[] = [];
  dates: Date;

  countedPlans;
  fromDate;
  tillDate;

  dateTimeFrom: string;
	dateTimeTo: string;
	searchFrom: string;
	searchTo: string;
	filters: any = {};

  title: string;
  description: string;
  no: string;
  yes: string;

  displayMonth: string;
  displayYear: number;
  displayDate;

  constructor(private planService: PlanService,
              private router: Router,
              private route: ActivatedRoute,
		          public translate: I18nService,
              private modalService: NgbModal,
		          private notificationService: NotificationsService,
              public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  setTimeToSearch(): void {
		if (this.dateTimeFrom) {
		  const date = new Date(this.dateTimeFrom);
		  this.searchFrom = new Date(date.getTime() - (new Date(this.dateTimeFrom).getTimezoneOffset() * 60000)).toISOString();
		} else if (this.dateTimeTo) {
		  const date = new Date(this.dateTimeTo);
		  this.searchTo = new Date(date.getTime() - (new Date(this.dateTimeTo).getTimezoneOffset() * 60000)).toISOString();
		}
	  }

	  getFilteredEvents(data: any = {}) :void {
		this.setTimeToSearch();

      let params = {
        fromDate: this.searchFrom,
        tillDate: this.searchTo,
        page: data.page || this.pagination.currentPage,
        itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
      }
      if(this.searchFrom != null || this.searchTo != null) {
        this.planService.list(params).subscribe(res => {
          if(res && res.data) {
            this.fromDate = res.data.fromDate;
            this.tillDate = res.data.tillDate;
            this.plans = res.data.items || [];
            this.pagination = res.data.pagedSummary;
          }
        })
      }
	  }

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date)
    }

    const request = {
      date:  this.selectedDay,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }

    this.planService.getByDate(request).subscribe(response => {
      if (response.success) {
        this.plans = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  clearFields(){
		  
		this.dateTimeFrom = '';
		this.dateTimeTo = '';
		this.searchFrom = '';
		this.searchTo = '';
		
		this.getListByDate();
	}

  list(data: any = {}): void {
    this.isLoading = true;
    this.selectedDay = null;

    if (data.fromDate != null && data.tillDate != null) {
      this.tillDate = data.tillDate,
      this.fromDate = data.fromDate
    }
    if (data.displayMonth != null && data.displayYear != null) {
      this.displayMonth = data.displayMonth;
      this.displayYear = data.displayYear;
    }

    const request = {
      fromDate: this.parseDates(this.fromDate),
      tillDate: this.parseDates(this.tillDate),
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }

    this.planService.list(request).subscribe(response => {
      if (response.success) {
        this.plans = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
        this.selectedDay = null;
      }
    });
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

  getHeaders(name: string): void {
		this.translateData();
    let table = document.getElementById('plans-table');
		let headersHtml = table.getElementsByTagName('th');
		let headersDto = ['name', 'description', 'fromDate', 'tillDate'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
      fromDate: this.searchFrom || null,
      tillDate: this.searchTo || null
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      		this.translate.get('print.select-file-format')
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
		this.planService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		});
	}

  openDeleteModal(id){
    forkJoin([
			this.translate.get('plans.remove'),
			this.translate.get('plans.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true});
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(() => this.delete(id), () => {});
 }

 delete(id){
  this.isLoadingCalendar = false;
   this.planService.delete(id).subscribe(() => {
    forkJoin([
      this.translate.get('modal.success'),
      this.translate.get('plans.succes-remove-msg'),
    ]).subscribe(([title, description]) => {
      this.title = title;
      this.description = description;
      });
     this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
     this.list();
     this.isLoadingCalendar = true;
   })
 }

  navigate(id) {
    this.router.navigate(['plan/', id, 'overview'], { relativeTo: this.route });
  }

  getListOfCoutedPlans(data) {
    this.isLoadingCountedTests = true;

    const request = {
      fromDate: this.parseDates(data.fromDate),
      tillDate: this.parseDates(data.tillDate)
    }

    this.planService.getPlanCount(request).subscribe(response => {
      if (response.success) {
        this.countedPlans = response.data;

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
    })
  }

}

