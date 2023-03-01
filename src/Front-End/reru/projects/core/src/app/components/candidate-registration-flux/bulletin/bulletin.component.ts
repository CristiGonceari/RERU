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
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
  selector: 'app-bulletin',
  templateUrl: './bulletin.component.html',
  styleUrls: ['./bulletin.component.scss']
})
export class BulletinComponent implements OnInit {

  bulletinForm: FormGroup;

  isLoading: boolean = true;
  toAddOrUpdateButton: boolean;

  userId;
  existentBulletin;
  bulletinId;
  contractorId;

  bulletinValuesLoading: boolean = true;
  bulletinIdnp;

  stepId: number;
  registrationFluxStep;
  isDone: boolean;

  title: string;
  description: string;

  abreviation = {
    city: 'or.',
    street: 'str.',
    boulevard: 'bl.',
    apartment: 'ap.',
    postCode: 'Post Code'
  }

  constructor(private fb: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private bulletinService: BulletinService,
    private notificationService: NotificationsService,
    private userProfile: UserProfileService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm(this.userId);
    this.getUserGeneralDatas(this.userId);
    this.translateData();
    this.subscribeForLanguageChange();
  }

  ngOnDestroy() {
    // clear message
    this.ds.clearData();
  }

  getUserGeneralDatas(userId) {
    this.userProfile.getCandidateProfile(userId).subscribe(res => {
      this.bulletinId = res.data.bulletinId;
      this.bulletinIdnp = res.data.idnp;
      this.contractorId = res.data.contractorId;

      this.bulletinValuesLoading = false;

      if (this.bulletinId != 0) {
        this.getExistentBulletin(this.contractorId);
      } else {
        this.toAddOrUpdateButton = false;
        this.isLoading = false;
      }

      this.getExistentStep(this.stepId, this.contractorId);
    })

  }

  getExistentStep(stepId, contractorId) {
    const request = {
      contractorId: contractorId,
      step: stepId
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  getExistentBulletin(userId) {
    this.bulletinService.get(userId).subscribe(res => {

      this.existentBulletin = res.data;
      let birthPlace = this.existentBulletin.birthPlace;
      let parentsResidenceAddress = this.existentBulletin.parentsResidenceAddress;
      let residenceAddress = this.existentBulletin.residenceAddress;

      this.initExistentForm(this.userId, this.bulletinId, this.existentBulletin, birthPlace, residenceAddress, parentsResidenceAddress);
      this.toAddOrUpdateButton = true;
      this.isLoading = false;
    })
  }

  parseAddress(data) {
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

  initForm(contractorId?: number): void {

    this.bulletinForm = this.fb.group({
      releaseDay: this.fb.control(null, [Validators.required]),
      series: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      emittedBy: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      contractorId: this.fb.control(contractorId, []),
      birthPlace: this.buildAddress(),
      parentsResidenceAddress: this.buildAddress(),
      residenceAddress: this.buildAddress()
    });
  }

  addressValidation(address)
  {
    return address.country && 
    address.region && 
    address.city 
       ? 'is-valid' : 'is-invalid';
  }

  initExistentForm(contractorId?: number, bulletinId?, existentBulletin?, birthPlace?, residenceAddress?, parentsResidenceAddress?): void {

    this.bulletinForm = this.fb.group({
      releaseDay: this.fb.control((existentBulletin && existentBulletin.releaseDay) || null, [Validators.required]),
      series: this.fb.control((existentBulletin && existentBulletin.series) || null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      emittedBy: this.fb.control((existentBulletin && existentBulletin.emittedBy) || null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
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
      region: this.fb.control(data.region, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      city: this.fb.control(data.city, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')])
      // street: this.fb.control(data.street, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // building: this.fb.control(data.building, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // apartment: this.fb.control(data.apartment, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // postCode: this.fb.control(data.postCode, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')])

    });
  }

  buildExistentAddress(data: AddressModel = <AddressModel>{}): FormGroup {
    return this.fb.group({
      id: this.fb.control(data.id, []),
      country: this.fb.control(data.country || 'Moldova', [Validators.required]),
      region: this.fb.control(data.region, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      city: this.fb.control(data.city, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // street: this.fb.control(data.street, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // building: this.fb.control(data.building, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // apartment: this.fb.control(data.apartment, [Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')]),
      // postCode: this.fb.control(data.postCode, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$')])

    });
  }

  openBulletinAddressModal(field: string): void {
    const modalRef = this.modalService.open(BulletinAddressModalComponent);
    if (this.existentBulletin != null) {
      modalRef.componentInstance.addressForm = this.buildExistentAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
      modalRef.result.then((address: AddressModel) => this.updateExistentAddress(address, field), () => { });
    } else {
      modalRef.componentInstance.addressForm = this.buildAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
      modalRef.result.then((address: AddressModel) => this.updateAddress(address, field), () => { });
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
			this.translate.get('bulletin.abbreviations.city'),
			this.translate.get('bulletin.abbreviations.street'),
			this.translate.get('bulletin.abbreviations.boulevard'),
			this.translate.get('bulletin.abbreviations.apartment'),
			this.translate.get('bulletin.abbreviations.post-code'),
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
    for (let prop in data) {
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
      birthPlace: this.parseAddress(data.birthPlace),
      parentsResidenceAddress: this.parseAddress(data.parentsResidenceAddress),
      residenceAddress: this.parseAddress(data.residenceAddress),
      contractorId: this.contractorId
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

  updateBulletin() {
    this.bulletinService.update(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.edit-bulletin-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, res.success, this.contractorId);

    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.edit-bulletin-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  createBulletin() {
    this.bulletinService.add(this.parseBulletin(this.bulletinForm.value)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.add-bulletin-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, res.success, this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.add-bulletin-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, error.success, this.contractorId);
    })
  }

  checkRegistrationStep(stepData, stepId, success, contractorId) {
    const datas = {
      isDone: success,
      stepId: this.stepId
    }
    if (stepData.length == 0) {
      this.addCandidateRegistationStep(success, stepId, contractorId);
      this.ds.sendData(datas);
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId) {
    const request = {
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }
    this.registrationFluxService.add(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId) {
    const request = {
      id: id,
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }

    this.registrationFluxService.update(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  inputValidator(form, field) {
    return form.get(field).valid
      ? 'is-valid' : 'is-invalid';
  }
}
