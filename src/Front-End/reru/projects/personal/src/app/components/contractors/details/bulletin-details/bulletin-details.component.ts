import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { BulletinAddressModalComponent } from 'projects/personal/src/app/utils/modals/bulletin-address-modal/bulletin-address-modal.component';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';
import { AddressModel, ContractorBulletinModel } from 'projects/personal/src/app/utils/models/bulletin.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { BulletinService } from 'projects/personal/src/app/utils/services/bulletin.service';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { I18nService } from 'projects/personal/src/app/utils/services/i18n.service';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { forkJoin } from 'rxjs';
import { DataService } from '../data.service';

@Component({
  selector: 'app-bulletin-details',
  templateUrl: './bulletin-details.component.html',
  styleUrls: ['./bulletin-details.component.scss']
})
export class BulletinDetailsComponent implements OnInit {
  @Input() contractor: Contractor;
  // bulletinForm: FormGroup;
  // bulletin: BulletinModel;
  // isLoading: boolean = true;

  bulletinForm: FormGroup;

  isLoading: boolean = true;
  toAddOrUpdateButton: boolean;
 
  existentBulletin;
  bulletinId;
  stepId;
  contractorId;

  bulletinValuesLoading: boolean = true;
  bulletinIdnp;
  
  registrationFluxStep;
  isDone:boolean;

  abreviation = {
    city: 'or.',
    street: 'str.',
    boulevard: 'bl.',
    apartment: 'ap.',
    postCode: 'Post Code'
  }

  constructor(private bulletinService: BulletinService,
              private fb: FormBuilder,
              private modalService: NgbModal,
              private contractorService: ContractorService,
              private notificationService: NotificationsService,
              private ds: DataService,
              private registrationFluxService: RegistrationFluxStepService,
              private route: ActivatedRoute,
              private translate: I18nService
    ) { }

  ngOnInit(): void {
    // this.retrieveBulletin();
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.subscribeForUser();

    this.translateData();
    this.subscribeForLanguageChange();
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  subscribeForUser(){
    this.getUser(this.contractorId)
    this.getExistentStep(this.stepId, this.contractor.id);
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.contractor = response.data;
      this.subscribeForBulletin(response.data)
    });
  }

  subscribeForBulletin(contractor){
    if (contractor.hasBulletin){
      this.getExistentBulletin(contractor.id);
    }else{
      this.toAddOrUpdateButton = false;
      this.isLoading = false;
    }
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
      this.bulletinId = res.data.id;
      let birthPlace = this.existentBulletin.birthPlace;
      let parentsResidenceAddress = this.existentBulletin.parentsResidenceAddress;
      let residenceAddress = this.existentBulletin.residenceAddress;

      this.initExistentForm(this.contractor.id, this.bulletinId, this.existentBulletin, birthPlace, residenceAddress, parentsResidenceAddress);
      this.toAddOrUpdateButton = true;
    })
  }

  addressValidation(address)
  {
    return address.country && 
    address.region && 
    address.city && 
    address.postCode 
       ? 'is-valid' : 'is-invalid';
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
    this.isLoading = false;
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
    if (this.existentBulletin != null){
      modalRef.componentInstance.addressForm = this.buildExistentAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
      modalRef.result.then((address: AddressModel) => this.updateExistentAddress(address, field), () => {});
    }else{
      modalRef.componentInstance.addressForm = this.buildAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
      modalRef.result.then((address: AddressModel) => this.updateAddress(address, field), () => {});
    }
  }

  updateAddress(address: AddressModel, field: string): void {
    this.bulletinForm.controls[field] = this.buildAddress(address);
    this.bulletinForm.updateValueAndValidity();
  }

  updateExistentAddress(address: AddressModel, field: string): void {
    this.bulletinForm.controls[field] = this.buildExistentAddress(address);
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
      return `${data.country || ''}, ${data.region ? data.region + ',' : ''} ${data.city ? this.abreviation.city + data.city + ',' : ''} ${data.street ? this.abreviation.street + data.street + ',' : ''} ${data.building ? this.abreviation.boulevard + data.building + ',' : ''} ${data.apartment ? this.abreviation.apartment + data.apartment + ',' : ''} ${data.postCode ? this.abreviation.postCode + data.postCode : ''}`.trim().replace(/(^\,)|(\,$)/g, '');
    }

    return `${data.country || ''}, ${data.city ? this.abreviation.city + data.city + ',' : ''} ${data.street ? this.abreviation.street + data.street + ',' : ''} ${data.building ? this.abreviation.boulevard + data.building + ',' : ''} ${data.apartment ? this.abreviation.apartment + data.apartment + ',' : ''}  ${data.postCode ? this.abreviation.postCode + data.postCode : ''}`.trim().replace(/(^\,)|(\,$)/g, '')
  }

  translateData(): void {
		forkJoin([
			this.translate.get('entity.bulletin.abbreviations.city'),
			this.translate.get('entity.bulletin.abbreviations.street'),
			this.translate.get('entity.bulletin.abbreviations.boulevard'),
			this.translate.get('entity.bulletin.abbreviations.apartment'),
			this.translate.get('entity.bulletin.abbreviations.post-code'),
		]).subscribe(
			([ city, street, boulevard, apartment, postCode	]) => {
				this.abreviation.city = city;
				this.abreviation.street = street;
				this.abreviation.boulevard = boulevard;
				this.abreviation.apartment = apartment;
				this.abreviation.postCode = postCode;
			}
		);
	}

	subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
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
  
  parseBulletin(data: ContractorBulletinModel): ContractorBulletinModel {
    return ObjectUtil.preParseObject({
        id: data.id,
        series: data.series,
        releaseDay: data.releaseDay,
        emittedBy: data.emittedBy,
        birthPlace: this.parseAddress(data.birthPlace),
        parentsResidenceAddress: this.parseAddress(data.parentsResidenceAddress) ,
        residenceAddress: this.parseAddress(data.residenceAddress),
        contractorId: this.contractor.id
    })
  }

  parseAdresses(data: AddressModel): AddressModel {
    return ObjectUtil.preParseObject({
        id: data.id,
        country: data.country,
        region: data.region,
        city: data.city,
        street: data.street,
        building: data.building,
        apartment: data.apartment,
        postCode: data.postCode
    })
  }

  updateBulletin(){
    this.bulletinService.update(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      this.notificationService.success('Success', 'Bulletin was updated!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , res.success, this.contractor.id);

      },error => {
      this.notificationService.error('Error', 'Bulletin was not updated!', NotificationUtil.getDefaultMidConfig());
      })
  }

  createBulletin(){
    this.bulletinService.addContractor(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      this.notificationService.success('Success', 'Bulletin was added!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , res.success, this.contractor.id);
     },error => {
      this.notificationService.error('Success', 'Bulletin was not added!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , error.success, this.contractor.id);
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
    this.getUser(this.contractor.id);
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

  // initForm(data: BulletinModel): void {
  //   this.bulletinForm = this.fb.group({
  //     id: this.fb.control(data.id),
  //     idnp: this.fb.control(data.idnp, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
  //     releaseDay: this.fb.control(data.releaseDay, [Validators.required]),
  //     series: this.fb.control(data.series, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
  //     emittedBy: this.fb.control(data.emittedBy, [Validators.required, Validators.pattern(/^[a-zA-Z0-9-. ]+$/)]),
  //     contractorId: this.fb.control(data.contractorId, [Validators.required]),
  //     birthPlace: this.buildAddress(data.birthPlace),
  //     livingAddress: this.buildAddress(data.livingAddress),
  //     residenceAddress: this.buildAddress(data.residenceAddress)
  //   });
  //   this.isLoading = false;
  // }

  // buildAddress(data: AddressModel = <AddressModel>{}): FormGroup {
  //   return this.fb.group({
  //     id: this.fb.control(data.id),
  //     country: this.fb.control(data.country, [Validators.required]),
  //     region: this.fb.control(data.region, []),
  //     city: this.fb.control(data.city, []),
  //     street: this.fb.control(data.street, []),
  //     building: this.fb.control(data.building, []),
  //     apartment: this.fb.control(data.apartment, [])
  //   });
  // }

  // openBulletinAddressModal(field: string): void {
  //   const modalRef = this.modalService.open(BulletinAddressModalComponent);
  //   modalRef.componentInstance.addressForm = this.buildAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
  //   modalRef.result.then((address: AddressModel) => this.updateAddress(address, field), () => {});
  // }

  // updateAddress(address: AddressModel, field: string): void {
  //   this.bulletinForm.controls[field] = this.buildAddress(address);
  // }

  // retrieveBulletin(): void {
  //   this.bulletinService.get(this.contractor.id).subscribe(response => {
  //     this.bulletin = {...response.data};
  //     this.initForm(response.data);
  //   });
  // }

  // submit(): void {
  //   this.isLoading = true;
  //   const request = ContractorParser.parseBulletin(this.bulletinForm.getRawValue());
  //   this.bulletinService.update(request).subscribe(response => {
  //     this.isLoading = false;
  //     this.contractorService.fetchContractor.next();
  //     this.notificationService.success('Success', 'Bulletin updated!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  resetBulletin(): void {
    this.isLoading = true;
    if (this.contractor.hasBulletin){
      this.getExistentBulletin(this.contractor.id);
      this.isLoading = false;
    }else{
      this.initForm(this.contractor.id)
      this.isLoading = false;
    }
  }

  // renderBirthPlace(): string {
  //   const birthPlace: AddressModel = (<FormGroup>this.bulletinForm.get('birthPlace')).getRawValue();
  //   return ContractorParser.renderAddressOrder(birthPlace);
  // }

  // renderLivingAddress(): string {
  //   const livingAddress: AddressModel = (<FormGroup>this.bulletinForm.get('livingAddress')).getRawValue();
  //   return ContractorParser.renderAddressOrder(livingAddress);
  // }

  // residenceAddress(): string {
  //   const residenceAddress: AddressModel = (<FormGroup>this.bulletinForm.get('residenceAddress')).getRawValue();
  //   return ContractorParser.renderAddressOrder(residenceAddress);
  // }

  // isInvalidPattern(field: string): boolean {
  //   return ValidatorUtil.isInvalidPattern(this.bulletinForm, field);
  // }
}
