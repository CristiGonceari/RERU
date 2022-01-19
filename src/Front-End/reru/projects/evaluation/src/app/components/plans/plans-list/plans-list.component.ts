import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute, CanActivate } from '@angular/router';
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
import { Events } from '../../../utils/models/calendar/events';
import { NullTemplateVisitor } from '@angular/compiler';

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

  plans: any[] = [];
  pagination: PaginationModel = new PaginationModel();
  eventuri: any[] = [];
  dates: Date;

  countedPlans;
  fromDate;
  tillDate;

  title: string;
  description: string;
  no: string;
  yes: string;

  displayMonth: string;
  displayYear: number;

  constructor(private planService: PlanService,
              private router: Router,
              private route: ActivatedRoute,
		          public translate: I18nService,
              private modalService: NgbModal,
		          private notificationService: NotificationsService,
              public dialog: MatDialog,
              private modal: NgbModal) { }

  ngOnInit(): void {
  }

  getListByDate(data: any = {}): void {

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
    }

    this.isLoading = true;

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

  list(data: any = {}): void {
    this.selectedDay = null;
    this.isLoading = true;
    
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
   })
 }

  navigate(id) {
    this.router.navigate(['plan/', id, 'overview'], { relativeTo: this.route });
  }

  getListOfCoutedPlans(data) {
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
    })
  }

}

