import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BulletinAddressModalComponent } from 'projects/personal/src/app/utils/modals/bulletin-address-modal/bulletin-address-modal.component';
import { AddressModel } from '../../../../utils/models/bulletin.model';
import { ContractorParser } from '../add.parser';
import { ValidatorUtil } from '../../../../utils/util/validator.util';

@Component({
  selector: 'app-bulletin-data-form',
  templateUrl: './bulletin-data-form.component.html',
  styleUrls: ['./bulletin-data-form.component.scss']
})
export class BulletinDataFormComponent implements OnInit {
  bulletinForm: FormGroup;
  isLoading: boolean;
  constructor(private fb: FormBuilder,
              private modalService: NgbModal) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.bulletinForm = this.fb.group({
      idnp: this.fb.control(null, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      releaseDay: this.fb.control(null, [Validators.required]),
      series: this.fb.control(null, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-. ]+$/)]),
      emittedBy: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z0-9-. ]+$/)]),
      contractorId: this.fb.control(null, []),
      birthPlace: this.buildAddress(),
      livingAddress: this.buildAddress(),
      residenceAddress: this.buildAddress()
    });
  }

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.bulletinForm, field);
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
    this.bulletinForm.updateValueAndValidity();
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
}
