import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorUtil } from '../../../utils/util/validator.util';
import { AddressModel, BulletinModel } from '../../../utils/models/bulletin.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BulletinAddressModalComponent } from '../../../utils/modals/bulletin-address-modal/bulletin-address-modal.component';
import { ActivatedRoute } from '@angular/router';
import { BulletinService } from '../../../utils/services/bulletin.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-bulletin',
  templateUrl: './bulletin.component.html',
  styleUrls: ['./bulletin.component.scss']
})
export class BulletinComponent implements OnInit {
  
  bulletinForm: FormGroup;

  isLoading: boolean;
  toAddOrUpdateButton: boolean;

  userId;
  existentBulletin;
  bulletinId;
  contractorId;

  bulletinValuesLoading: boolean = true;
  bulletinIdnp;
  
  stepId;
  registrationFluxStep;
  isDone:boolean;

  constructor(private fb: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private bulletinService: BulletinService,
    private notificationService: NotificationsService,
    private userProfile: UserProfileService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService
    ) { }

  ngOnInit(): void {
    this.userId =parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm(this.userId);
    this.getUserGeneralDatas(this.userId);
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  getUserGeneralDatas(userId){
    this.userProfile.getCandidateProfile(userId).subscribe(res => {
      this.bulletinId = res.data.bulletinId;
      this.bulletinIdnp = res.data.idnp;
      this.contractorId = res.data.contractorId;

        this.bulletinValuesLoading = false;

      if (this.bulletinId != 0){
        this.getExistentBulletin(this.userId);
      }else{
        this.toAddOrUpdateButton = false;
      }

      this.getExistentStep(this.stepId, this.contractorId);
    })

  }

  getExistentStep(stepId, contractorId){
    const request = {
      contractorId : contractorId,
      step: stepId
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  getExistentBulletin(userId){
    this.bulletinService.get(userId).subscribe(res => {

      this.existentBulletin = res.data;
      let birthPlace = this.existentBulletin.birthPlace;
      let parentsResidenceAddress = this.existentBulletin.parentsResidenceAddress;
      let residenceAddress = this.existentBulletin.residenceAddress;

      this.initExistentForm(this.userId, this.bulletinId, this.existentBulletin, birthPlace, residenceAddress, parentsResidenceAddress);
        this.toAddOrUpdateButton = true;
    })
  }

  parseAddress(data){
    return {
      id: data.id,
      country: data.country, 
      region: data.region,
      city: data.city,
      street: data.street,
      building: data.building,
      apartment: data.apartment,
      postCode: data.postCode

    };
  }

  initForm(contractorId? : number): void {
   
    this.bulletinForm = this.fb.group({
      releaseDay: this.fb.control(null, [Validators.required]),
      series: this.fb.control(null, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      emittedBy: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z0-9-. ]+$/)]),
      contractorId: this.fb.control(contractorId, []),
      birthPlace: this.buildAddress(),
      parentsResidenceAddress: this.buildAddress(),
      residenceAddress: this.buildAddress()
    });
  }

  initExistentForm(contractorId? : number, bulletinId?, existentBulletin?, birthPlace?, residenceAddress? , parentsResidenceAddress?): void {
   
    this.bulletinForm = this.fb.group({
      releaseDay: this.fb.control((existentBulletin && existentBulletin.releaseDay) || null, [Validators.required]),
      series: this.fb.control((existentBulletin && existentBulletin.series) || null, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      emittedBy: this.fb.control( (existentBulletin && existentBulletin.emittedBy) || null, [Validators.required, Validators.pattern(/^[a-zA-Z0-9-. ]+$/)]),
      contractorId: this.fb.control(contractorId, []),
      id: this.fb.control(bulletinId, []),
      birthPlace: this.buildExistentAddress(this.parseAddress(birthPlace)),
      parentsResidenceAddress: this.buildExistentAddress(this.parseAddress(residenceAddress)),
      residenceAddress: this.buildExistentAddress(this.parseAddress(parentsResidenceAddress))
    });
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.bulletinForm, field);
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.bulletinForm, field);
  }

  buildAddress(data: AddressModel = <AddressModel>{}): FormGroup {
    return this.fb.group({
      country: this.fb.control(data.country || 'Moldova', [Validators.required]),
      region: this.fb.control(data.region, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      city: this.fb.control(data.city,[Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      street: this.fb.control(data.street, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      building: this.fb.control(data.building, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      apartment: this.fb.control(data.apartment, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      postCode: this.fb.control(data.postCode, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)])

    });
  }

  buildExistentAddress(data: AddressModel = <AddressModel>{}): FormGroup {
    return this.fb.group({
      id: this.fb.control(data.id, []),
      country: this.fb.control(data.country || 'Moldova', [Validators.required]),
      region: this.fb.control(data.region, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      city: this.fb.control(data.city,[Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      street: this.fb.control(data.street, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      building: this.fb.control(data.building, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      apartment: this.fb.control(data.apartment, [Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      postCode: this.fb.control(data.postCode, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)])

    });
  }

  openBulletinAddressModal(field: string): void {
    const modalRef = this.modalService.open(BulletinAddressModalComponent);
    modalRef.componentInstance.addressForm = this.buildAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
    modalRef.result.then((address: AddressModel) => this.updateAddress(address, field), () => {});
  }

  updateAddress(address: AddressModel, field: string): void {
    this.bulletinForm.controls[field] = this.buildAddress(address);
    this.bulletinForm.updateValueAndValidity();
  }

  renderBirthPlace(): string {
    const birthPlace: AddressModel = (<FormGroup>this.bulletinForm.get('birthPlace')).getRawValue();
    
    return this.renderAddressOrder(birthPlace);
  }

  renderParentResidenceAddress(): string {
    const livingAddress: AddressModel = (<FormGroup>this.bulletinForm.get('parentsResidenceAddress')).getRawValue();

    return this.renderAddressOrder(livingAddress);
  }

  renderResidenceAddress(): string {
    const residenceAddress: AddressModel = (<FormGroup>this.bulletinForm.get('residenceAddress')).getRawValue();

    return this.renderAddressOrder(residenceAddress);
  }

  renderAddressOrder(data: AddressModel): string {
    if (this.hasCountryOnly(data)) {
      return data.country;
    }

    if (!this.hasAddressData(data)) {
      return '-';
    }

    if (data.region) {
      return `${data.country || ''}, ${data.region ? data.region + ',' : ''} ${data.city ? 'or. ' + data.city + ',' : ''} ${data.street ? 'str ' + data.street + ',' : ''} ${data.building ? 'bl. ' + data.building + ',' : ''} ${data.apartment ? 'ap. ' + data.apartment + ',' : ''} ${data.postCode ? 'Post Code. ' + data.postCode : ''}`.trim().replace(/(^\,)|(\,$)/g, '');
    }

    return `${data.country || ''}, ${data.city ? 'or. ' + data.city + ',' : ''} ${data.street ? 'str ' +data.street + ',' : ''} ${data.building ? 'bl. ' + data.building + ',' : ''} ${data.apartment ? 'ap. ' + data.apartment + ',' : ''}  ${data.postCode ? 'Post Code. ' + data.postCode : ''}`.trim().replace(/(^\,)|(\,$)/g, '')
  }

   hasCountryOnly(data: AddressModel): boolean {
    if (!data.city && !data.street && !data.building && !data.apartment && data.country) {
      return true;
    }

    return false;
  }

   hasAddressData(data: AddressModel): boolean {
    for(let prop in data) {
      if (data[prop]) return true;
    }

    return false;
  }
  
  parseBulletin(data: BulletinModel): BulletinModel {
    return ObjectUtil.preParseObject({
        id: data.id,
        series: data.series,
        releaseDay: data.releaseDay,
        emittedBy: data.emittedBy,
        birthPlace: data.birthPlace,
        parentsResidenceAddress: data.parentsResidenceAddress,
        residenceAddress: data.residenceAddress,
        contractorId: this.contractorId
    })
}

  updateBulletin(){
    this.bulletinService.update(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      this.notificationService.success('Success', 'Bulletin was updated!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , res.success, this.contractorId);

      },error => {
      this.notificationService.error('Error', 'Bulletin was not updated!', NotificationUtil.getDefaultMidConfig());
      })
  }

  createBulletin(){
    this.bulletinService.add(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      this.notificationService.success('Success', 'Bulletin was added!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , res.success, this.contractorId);
     },error => {
      this.notificationService.error('Success', 'Bulletin was not added!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , error.success, this.contractorId);
     })
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
  
}
