import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddressModel, BulletinModel } from '../../../utils/models/bulletin.model';
import { Contractor } from '../../../utils/models/contractor.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';
import { ContractorParser } from '../../contractors/add/add.parser';

@Component({
  selector: 'app-profile-bulletin',
  templateUrl: './profile-bulletin.component.html',
  styleUrls: ['./profile-bulletin.component.scss']
})
export class ProfileBulletinComponent implements OnInit {
  @Input() contractor: Contractor;
  bulletinForm: FormGroup;
  bulletin: BulletinModel;
  isLoading: boolean = true;
  constructor(private contractorProfileService: ContractorProfileService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.retrieveBulletin();
  }

  initForm(data: BulletinModel): void {
    this.bulletinForm = this.fb.group({
      id: this.fb.control(data.id),
      idnp: this.fb.control({value: data.idnp, disabled: true }, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      releaseDay: this.fb.control({value: data.releaseDay, disabled: true }, [Validators.required]),
      series: this.fb.control({value: data.series, disabled: true }, [Validators.required]),
      emittedBy: this.fb.control({value: data.emittedBy, disabled: true }, [Validators.required]),
      contractorId: this.fb.control(data.contractorId, [Validators.required]),
      birthPlace: this.buildAddress(data.birthPlace),
      livingAddress: this.buildAddress(data.livingAddress),
      residenceAddress: this.buildAddress(data.residenceAddress)
    });
    this.isLoading = false;
  }

  buildAddress(data: AddressModel = <AddressModel>{}): FormGroup {
    return this.fb.group({
      id: this.fb.control(data && data.id),
      country: this.fb.control({value: data && data.country, disabled: true }, [Validators.required]),
      region: this.fb.control({value: data && data.region, disabled: true }, [Validators.required]),
      city: this.fb.control({value: data && data.city, disabled: true }, [Validators.required]),
      street: this.fb.control({value: data && data.street, disabled: true }, [Validators.required]),
      building: this.fb.control({value: data && data.building, disabled: true }, [Validators.required]),
      apartment: this.fb.control({value: data && data.apartment, disabled: true }, [Validators.required])
    });
  }

  retrieveBulletin(): void {
    this.contractorProfileService.getBulletin({}).subscribe(response => {
      this.bulletin = {...response.data};
      this.initForm(response.data);
    });
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
