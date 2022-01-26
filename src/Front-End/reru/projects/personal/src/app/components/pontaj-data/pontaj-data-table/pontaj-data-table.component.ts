import { Component, OnInit, } from '@angular/core';
import { TimesheetdatesService } from '../../../utils/services/timesheetdates.service';
import { TimeSheetTableValuesService } from '../../../utils/services/time-sheet-table-values.service';
import { ContractorTimesheetDataModel } from '../../../utils/models/contractor-timesheet-data.model'
import { ObjectUtil } from '../../../utils/util/object.util';
import { MonthsEnum } from '../../../utils/models/months.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';;
import { AddEditTimesheetValuesComponent } from '../../../utils/modals/add-edit-timesheet-values/add-edit-timesheet-values.component';
import { AddEditTimeSheetTableModel, TimesheetDataModel } from '../../../utils/models/timesheet-data.model';

@Component({
  selector: 'app-pontaj-data-table',
  templateUrl: './pontaj-data-table.component.html',
  styleUrls: ['./pontaj-data-table.component.scss']
})
export class PontajDataTableComponent implements OnInit {

  contractorTimesheetTable: ContractorTimesheetDataModel[];
  addEditValue: AddEditTimeSheetTableModel;
  monthsEnum = MonthsEnum;
  data: any = {};

  observable1: any;
  observable2: any;
  observable3: any;
  observable4: any;

  pagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };

  isLoading: boolean = true;
  contractorName: string;
  filters: any = {};
  departmentId: any;

  month: number;
  year: number;
  firstDayOfMonth: string;
  lastDayOfMonth: string;
  daylist: any;

  constructor(
    private timesheetdatesService: TimesheetdatesService,
    private timeSheetTableValuesService: TimeSheetTableValuesService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.timesheetdatesService.department.subscribe(
      x => this.departmentId = x);
    this.subscribeParams();
  }

  getArrayOdDays(fromDate, toDate) {
    let dayArray = [];
    for (let currentDay = fromDate.getDate(); currentDay <= toDate.getDate(); currentDay++) {
      dayArray.push(currentDay);
    }
    return dayArray;
  }

  subscribeParams() {
    this.observable1 = this.timesheetdatesService.tableMounth.asObservable().subscribe((value: any) => {
      this.month = value;
      this.isLoading = true;
    })
    this.observable2 = this.timesheetdatesService.tableYear.asObservable().subscribe((value: any) => {
      this.year = value;
      this.isLoading = true;
    })
    this.observable3 = this.timesheetdatesService.firstDayOfMonth.asObservable().subscribe((value: any) => {
      this.firstDayOfMonth = value;
      this.isLoading = true;
    })
    this.observable4 = this.timesheetdatesService.lastDayOfMonth.asObservable().subscribe((value: any) => {
      this.lastDayOfMonth = value;

      setTimeout(() => {
        this.calculateDays();
      }, 1);

      this.isLoading = true;
    })
  }

  calculateDays(): void {
    this.daylist = this.getArrayOdDays(new Date(this.firstDayOfMonth), new Date(this.lastDayOfMonth));
    this.list();
    this.departmentId = null;
  }

  setKeyword(value: string): void {
    this.contractorName = value;
  }

  openAddEditValuesModal(contractor: ContractorTimesheetDataModel, value: TimesheetDataModel): void {
    const modalRef = this.modalService.open(AddEditTimesheetValuesComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.componentInstance.value = value;
    modalRef.result.then(() => this.list(), () => { });
  }

  list(data: any = {}): void {
    if (data.hasOwnProperty('contractorName')) this.contractorName = data.contractorName;
    data = ObjectUtil.preParseObject({
      ...data,
      page: data.page || this.pagedSummary.currentPage,
      contractorName: data.contractorName || this.contractorName,
      fromDate: this.firstDayOfMonth,
      toDate: this.lastDayOfMonth,
      DepartmentId: this.departmentId || data.departmentId,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
      ...this.filters
    })
    this.data = data;
    ;
    this.timeSheetTableValuesService.list(data).subscribe(response => {
      if (response.success) {
        this.contractorTimesheetTable = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  getCredentials(value: string){
    return value.length > 7 ? value.substring(0, 7) + "..." : value.substring(0, 7);
  }

  ngOnDestroy() {
    this.observable1.unsubscribe();
    this.observable2.unsubscribe();
    this.observable3.unsubscribe();
    this.observable4.unsubscribe();
  }

}
