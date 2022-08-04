import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { MilitaryObligationService } from 'projects/personal/src/app/utils/services/military-obligation.service';
import { Observable, Subject } from 'rxjs';
import { DataService } from '../data.service';
import { MilitaryObligationModel } from 'projects/personal/src/app/utils/models/military-obligation.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';

@Component({
  selector: 'app-ranks',
  templateUrl: './ranks.component.html',
  styleUrls: ['./ranks.component.scss']
})
export class RanksComponent implements OnInit {
  @Input() contractor: Contractor;
  
  isLoading: boolean = true;
   // ranks: RankModel[];
  // pagedSummary: PagedSummary = new PagedSummary();
  // contractorId: number;

  militaryObligationForm: FormGroup;
  stepId;

  addOrEditMilitaryObligationButton: boolean;
  isLoadingMilitaryObligation: boolean = true;
  militaryObligationData;
  registrationFluxStep;
  contractorId;

  militaryObligationTypeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];

  isDone:boolean;

  constructor(
    private notificationService: NotificationsService,
    private ds: DataService,
    private fb: FormBuilder,
    private militaryObligationService: MilitaryObligationService,
    private route: ActivatedRoute,
    private registrationFluxService: RegistrationFluxStepService,
    private referenceService: ReferenceService,
    private contractorService: ContractorService,
    ) { }

  ngOnInit(): void {
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.retrieveDropdowns();
    this.subscribeForParams();
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  subscribeForParams(): void {
    this.getUser(this.contractorId);

    this.getExistentStep(this.stepId, this.contractor.id);
    // this.subscribeForData();
    this.isLoading = false;
  }

getUser(id: number): void {
  this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
    this.contractor = response.data;
    this.subscribeForData(response.data)
  });
}

  initForm(data?): void {
    if(data == null){
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations()])
      });
    }
    else {
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations(data, this.contractor.id)])
      });
    }
    
  }
  
  retrieveDropdowns(): void {
    this.referenceService.getMilitaryObligationTypeEnum().subscribe(res => {
      this.militaryObligationTypeEnum = res.data;
    });
  }

  subscribeForData(contractor) {

      if (contractor.hasMilitaryObligations) {

        this.getMilitaryObligations(contractor.id);
      }
      else {
        this.addOrEditMilitaryObligationButton = false;
        this.isLoadingMilitaryObligation = false;
        this.militaryObligationData = null;
      }
  }

  getMilitaryObligations(contractorId) {
    this.isLoadingMilitaryObligation = true;

    const request = {
      contractorId: contractorId
    }

    this.militaryObligationService.list(request).subscribe(res => {
      this.militaryObligationData = res.data.items;
      this.initMilitaryObligationForm(this.militaryObligationData);
      this.isLoadingMilitaryObligation = false;
      this.addOrEditMilitaryObligationButton = true;
    })
  }

  initMilitaryObligationForm(obligation) {
    
    if (obligation != null) {

      for (let i = 0; i < obligation.length; i++) {

        if (i > 0) {

          this.addMilitaryObligation(obligation[i]);
          
        }else{
          this.initForm(obligation[i])
        }
      }
    }
  }

  generateMilitaryObligations(militaryObligation?, contractorId?) {
    
    return this.fb.group({
      id: this.fb.control((militaryObligation && militaryObligation.id) || null, []),
      militaryObligationType: this.fb.control((militaryObligation && militaryObligation.militaryObligationType) || null, [Validators.required]),
      mobilizationYear: this.fb.control((militaryObligation && militaryObligation.mobilizationYear) || null, [Validators.required]),
      withdrawalYear: this.fb.control((militaryObligation && militaryObligation.withdrawalYear) || null, [Validators.required]),
      efectiv: this.fb.control((militaryObligation && militaryObligation.efectiv) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      militarySpecialty: this.fb.control((militaryObligation && militaryObligation.militarySpecialty) || null, [Validators.required]),
      degree: this.fb.control((militaryObligation && militaryObligation.degree) || null, [Validators.required]),
      militaryBookletSeries: this.fb.control((militaryObligation && militaryObligation.militaryBookletSeries) || null, [Validators.required]),
      militaryBookletNumber: this.fb.control((militaryObligation && militaryObligation.militaryBookletNumber) || null, []),
      militaryBookletReleaseDay: this.fb.control((militaryObligation && militaryObligation.militaryBookletReleaseDay) || null, [Validators.required]),
      militaryBookletEminentAuthority: this.fb.control((militaryObligation && militaryObligation.militaryBookletEminentAuthority) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      contractorId: this.fb.control(contractorId || null, []),
    });
  }

  createMilitaryObligations(){
    this.buildMilitaryObligationForm().subscribe(response => {
      this.notificationService.success('Success', 'Military obligation relation added!', NotificationUtil.getDefaultMidConfig());
      this.getMilitaryObligations(this.contractor.id);
    }, error => {
      this.notificationService.error('Failure', 'Military obligation relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  buildMilitaryObligationForm(): Observable<any> {
    const request = this.parseMilitaryObligations(this.militaryObligationForm.getRawValue().obligations, this.contractor.id);
    return this.militaryObligationService.addMultiple(request);
  }

  parseMilitaryObligations(data: MilitaryObligationModel[], contractorId: number): MilitaryObligationModel[] {
    return data.map(el => this.parseMilitaryObligation(el, contractorId));
  }

  parseMilitaryObligation(data: MilitaryObligationModel, contractorId): MilitaryObligationModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      militaryObligationType: parseInt(data.militaryObligationType),
      mobilizationYear: data.mobilizationYear,
      withdrawalYear: data.withdrawalYear,
      efectiv : data.efectiv,
      militarySpecialty : data.militarySpecialty,
      degree : data.degree,
      militaryBookletSeries : data.militaryBookletSeries,
      militaryBookletNumber : data.militaryBookletNumber,
      militaryBookletReleaseDay : data.militaryBookletReleaseDay,
      militaryBookletEminentAuthority : data.militaryBookletEminentAuthority,
      contractorId: contractorId
    })
  }

  addMilitaryObligation(obligation?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (obligation == null) {
      (<FormArray>this.militaryObligationForm.controls.obligations).controls.push(this.generateMilitaryObligations());
    } else {
      (<FormArray>this.militaryObligationForm.controls.obligations).controls.push(
        this.generateMilitaryObligations(
          obligation,
          this.contractor.id
        )
      );
    }
  }

  removeMilitaryObligation(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.militaryObligationForm.controls.obligations).controls.splice(index, 1);
  }

  getExistentStep(step, contractorId){
    const request = {
      contractorId : contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep(){
    if(this.militaryObligationData != null){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractor.id);
    }
    else{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractor.id);
    }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId){
    const datas= {
      isDone: success,
      stepId: this.stepId
    }
    if(stepData.length == 0){
      this.addCandidateRegistationStep(success, stepId, contractorId);
      this.ds.sendData(datas);
    }else{
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId ){
    const request = {
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    
    this.registrationFluxService.update(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
  // subscribeForParams(): void {
  //   this.route.parent.params.subscribe(response => {
  //     this.contractorId = response.id;
  //     this.retriveRanks({contractorId: response.id});
  //   });
  // }

  // retriveRanks(data: any = {}): void {
  //   const request = {
  //     contractorId: this.contractorId,
  //     page: data.page || this.pagedSummary.currentPage,
  //     itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize 
  //   }

  //   this.rankService.list(request).subscribe((response: any) => {
  //     this.isLoading = false;
  //     this.ranks = response.data.items;
  //   });
  // }

  // openAddRankModal(): void {
  //   const modalRef = this.modalService.open(AddRankModalComponent);
  //   modalRef.componentInstance.contractorId = this.contractorId;
  //   modalRef.result.then((response: RankModel) => this.addRank(response), () => {})
  // }

  // addRank(data: RankModel): void {
  //   this.isLoading = true;
  //   this.rankService.create(data).subscribe(response => {
  //     this.retriveRanks();
  //     this.notificationService.success('Success', 'Rank added!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // openEditRankModal(rank: RankModel): void {
  //   const modalRef = this.modalService.open(EditRankModalComponent);
  //   modalRef.componentInstance.contractorId = this.contractorId;
  //   modalRef.componentInstance.rank = {...rank};
  //   modalRef.result.then((response: RankModel) => this.editRank(response), () => {})
  // }

  // editRank(data: RankModel): void {
  //   this.rankService.update(data).subscribe(response => {
  //     this.retriveRanks();
  //     this.notificationService.success('Success', 'Rank edited!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // openDeleteRankModal(rank: RankModel): void {
  //   const modalRef = this.modalService.open(DeleteRankModalComponent);
  //   modalRef.componentInstance.name = rank.rankRecordName;
  //   modalRef.result.then(() => this.deleteRank(rank.id), () => {})
  // }

  // deleteRank(id: number): void {
  //   this.rankService.delete(id).subscribe(response => {
  //     this.retriveRanks();
  //     this.notificationService.success('Success', 'Rank deleted!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }
}
