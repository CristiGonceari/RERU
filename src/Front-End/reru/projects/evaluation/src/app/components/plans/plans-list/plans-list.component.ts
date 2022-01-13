import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute, CanActivate } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PlansCalendarComponent } from './plans-calendar/plans-calendar.component';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'app-plans-list',
  templateUrl: './plans-list.component.html',
  styleUrls: ['./plans-list.component.scss'],
})


export class PlansListComponent implements OnInit {
 
  @ViewChild(PlansCalendarComponent)  currentMonth: boolean;

  selectedDay;
  date: Date;

  isLoading: boolean = true;
  isLoadingTable: boolean = true;

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

  getListByDate(data :any = {}): void
  { 
    if(data.clickedDay != null){
      this.selectedDay = data.clickedDay;
    }

    this.isLoadingTable = false;

    const request = {
      date: data.clickedDay || this.selectedDay,
      page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }
    
    this.planService.getByDate(request).subscribe(response => {
      if (response.success) {
        this.plans = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoadingTable = true;
      }
    });
  }

  list(data :any = {}): void {
    if (data.fromDate != null && data.tillDate != null){
      this.tillDate = data.tillDate,
      this.fromDate = data.fromDate
    }
    
    this.isLoading = true;
    const request = {
      fromDate:  this.parseDates(this.fromDate),
      tillDate:  this.parseDates(this.tillDate),
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

  parseDates(date){
      
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

}

