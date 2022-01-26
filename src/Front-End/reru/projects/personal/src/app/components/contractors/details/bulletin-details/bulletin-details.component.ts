import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { BulletinAddressModalComponent } from 'projects/personal/src/app/utils/modals/bulletin-address-modal/bulletin-address-modal.component';
import { AddressModel, BulletinModel } from 'projects/personal/src/app/utils/models/bulletin.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { BulletinService } from 'projects/personal/src/app/utils/services/bulletin.service';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { ContractorParser } from '../../add/add.parser';

@Component({
  selector: 'app-bulletin-details',
  templateUrl: './bulletin-details.component.html',
  styleUrls: ['./bulletin-details.component.scss']
})
export class BulletinDetailsComponent implements OnInit {
  @Input() contractor: Contractor;
  bulletinForm: FormGroup;
  bulletin: BulletinModel;
  isLoading: boolean = true;
  constructor(private bulletinService: BulletinService,
              private fb: FormBuilder,
              private modalService: NgbModal,
              private contractorService: ContractorService,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.retrieveBulletin();
  }

  initForm(data: BulletinModel): void {
    this.bulletinForm = this.fb.group({
      id: this.fb.control(data.id),
      idnp: this.fb.control(data.idnp, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      releaseDay: this.fb.control(data.releaseDay, [Validators.required]),
      series: this.fb.control(data.series, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      emittedBy: this.fb.control(data.emittedBy, [Validators.required, Validators.pattern(/^[a-zA-Z0-9-. ]+$/)]),
      contractorId: this.fb.control(data.contractorId, [Validators.required]),
      birthPlace: this.buildAddress(data.birthPlace),
      livingAddress: this.buildAddress(data.livingAddress),
      residenceAddress: this.buildAddress(data.residenceAddress)
    });
    this.isLoading = false;
  }

  buildAddress(data: AddressModel = <AddressModel>{}): FormGroup {
    return this.fb.group({
      id: this.fb.control(data.id),
      country: this.fb.control(data.country, [Validators.required]),
      region: this.fb.control(data.region, []),
      city: this.fb.control(data.city, []),
      street: this.fb.control(data.street, []),
      building: this.fb.control(data.building, []),
      apartment: this.fb.control(data.apartment, [])
    });
  }

  openBulletinAddressModal(field: string): void {
    const modalRef = this.modalService.open(BulletinAddressModalComponent);
    modalRef.componentInstance.addressForm = this.buildAddress((<FormGroup>this.bulletinForm.get(field)).getRawValue());
    modalRef.result.then((address: AddressModel) => this.updateAddress(address, field), () => {});
  }

  updateAddress(address: AddressModel, field: string): void {
    this.bulletinForm.controls[field] = this.buildAddress(address);
  }

  retrieveBulletin(): void {
    this.bulletinService.get(this.contractor.id).subscribe(response => {
      this.bulletin = {...response.data};
      this.initForm(response.data);
    });
  }

  submit(): void {
    this.isLoading = true;
    const request = ContractorParser.parseBulletin(this.bulletinForm.getRawValue());
    this.bulletinService.update(request).subscribe(response => {
      this.isLoading = false;
      this.contractorService.fetchContractor.next();
      this.notificationService.success('Success', 'Bulletin updated!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  resetBulletin(): void {
    this.isLoading = true;
    this.initForm(this.bulletin);
  }

  renderBirthPlace(): string {
    const birthPlace: AddressModel = (<FormGroup>this.bulletinForm.get('birthPlace')).getRawValue();
    return ContractorParser.renderAddressOrder(birthPlace);
  }

  renderLivingAddress(): string {
    const livingAddress: AddressModel = (<FormGroup>this.bulletinForm.get('livingAddress')).getRawValue();
    return ContractorParser.renderAddressOrder(livingAddress);
  }

  residenceAddress(): string {
    const residenceAddress: AddressModel = (<FormGroup>this.bulletinForm.get('residenceAddress')).getRawValue();
    return ContractorParser.renderAddressOrder(residenceAddress);
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.bulletinForm, field);
  }
}
