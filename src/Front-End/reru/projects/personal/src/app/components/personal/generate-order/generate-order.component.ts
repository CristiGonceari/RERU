import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PositionService } from '../../../utils/services/position.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { AddPositionComponent } from './add-position/add-position.component';
import { IndividualContractRequestComponent } from './individual-contract-request/individual-contract-request.component';
import { ContractModel, InstructionModel } from '../../../utils/models/contract.model';
import { combineLatest, forkJoin, Observable, of } from 'rxjs';
import { ContractService } from '../../../utils/services/contract.service';
import { delay } from 'rxjs/operators';
import { MatHorizontalStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-generate-order',
  templateUrl: './generate-order.component.html',
  styleUrls: ['./generate-order.component.scss']
})
export class GenerateOrderComponent implements AfterViewInit {
  @ViewChild('stepper') stepper: MatHorizontalStepper;
  @ViewChild(AddPositionComponent) addPositionComponent: AddPositionComponent;
  @ViewChild(IndividualContractRequestComponent) individualContractRequestComponent: IndividualContractRequestComponent;
  isLoading: boolean;
  isDisabledButton: boolean;
  isEditable: boolean = false;
  contractorId: number;
  completedEvents: boolean[] = [];
  constructor(private positionService: PositionService,
              private router: Router,
              private route: ActivatedRoute,
              private notificationService: NotificationsService,
              private contractService: ContractService,
              private cd: ChangeDetectorRef) { }

  ngAfterViewInit(): void {
    this.subscribeForParams();
    // this.cd.detectChanges();
  }

  subscribeForParams(): void {
    combineLatest([
      this.route.params,
      this.route.queryParams
    ]).subscribe(([params, query]) => {
      this.contractorId = params.id;
      if (query && query.step) {
        if (query.step!=0) this.addPositionComponent.isLoading = true;
        this.setCompleted(+(query.step));
        of(true).pipe(delay(300)).subscribe(() => {this.stepper.selectedIndex = +(query.step)});
        this.cd.detectChanges();
      }
    });
  }

  setCompleted(untilNumber: number): void {
    for(let i = 0; i < untilNumber; i++) {
      this.completedEvents[i] = true;
    }
  }

  createPosition(): void {
    this.addPositionComponent.isLoading = true;
    this.isEditable = false;
    this.buildPosition().subscribe(() => {
      this.notificationService.success('Success', 'Position added!', NotificationUtil.getDefaultMidConfig());
    }, () => {}, () => {
      this.addPositionComponent.isLoading = true;
    });
  }

  createContract(): void {
    this.individualContractRequestComponent.isLoading = true;
    this.isEditable = false;
    this.isDisabledButton = true;
    this.buildContract().subscribe(() => {
      this.isDisabledButton = false;
      this.notificationService.success('Success', 'Order generated', NotificationUtil.getDefaultConfig());
      this.router.navigate(['../../', this.contractorId], { relativeTo: this.route});
    }, () => {}, () => {
      this.isDisabledButton = false;
      this.individualContractRequestComponent.isLoading = true;
    });
  }

  buildPosition(): Observable<any> {
    const request = this.parsePosition(this.addPositionComponent.positionForm.getRawValue());
    return this.positionService.create(request);
  }

  buildContract(): Observable<any> {
    const request = this.parseContract(this.individualContractRequestComponent.individualContractForm.getRawValue());
    return this.contractService.create(request);
  }

  parsePosition(data) {
    return ObjectUtil.preParseObject({
      fromDate: data.fromDate,
      generatedDate: data.generatedDate,
      probationDayPeriod: data.probationDayPeriod,
      workHours: data.workHours ? +data.workHours : null,
      organizationRoleId: data.organizationRoleId ? +data.organizationRoleId : null,
      departmentId: data.departmentId ? +data.departmentId : null,
      contractorId: this.contractorId ? +this.contractorId : null,
      workPlace: data.workPlace
    });
  }

  parseContract(data): ContractModel {
    return ObjectUtil.preParseObject({
      superiorId: +data.superiorId,
      netSalary: +data.netSalary,
      brutSalary: +data.brutSalary,
      currencyTypeId: +data.currencyTypeId,
      contractorId: +this.contractorId,
      vacationDays: +data.vacationDays,
      instruction: this.parseInstruction(data.instruction)
    });
  }

  parseInstruction(data): InstructionModel {
    return ObjectUtil.preParseObject({
      thematic: data.thematic,
      instructorName: data.instructorName,
      instructorLastName: data.instructorLastName,
      duration: +data.duration,
      date: data.date,
      contractorId: +this.contractorId,
    });
  }

  get firstForm() {
    return this.addPositionComponent && this.addPositionComponent.positionForm;
  }

  set firstForm(data) {}

  get secondForm() {
    return this.individualContractRequestComponent && this.individualContractRequestComponent.individualContractForm;
  }

  set secondForm(data) {}
}
