import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SelectItem } from '../../../utils/models/select-item.model';
import { LocationService } from '../../../utils/services/location/location.service';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-edit-location',
  templateUrl: './add-edit-location.component.html',
  styleUrls: ['./add-edit-location.component.scss']
})
export class AddEditLocationComponent implements OnInit {

  	locationForm: FormGroup;
	locationId: number;
	locationName;
	types: SelectItem[] = [];
	isLoading: boolean = true;
  
  	constructor(
   		private formBuilder: FormBuilder,
		private locationService: LocationService,
		private location: Location,
		private activatedRoute: ActivatedRoute,
		private reference: ReferenceService,
		private notificationService: NotificationsService
  	) { }

  	ngOnInit(): void {
    	this.locationForm = new FormGroup({
			name: new FormControl(),
			address: new FormControl(),
			type: new FormControl(),
			places: new FormControl(),
			description: new FormControl()
		});
		this.initData();
		this.getDropdownData();
  	}

 	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.locationId = response.id;
				this.locationService.getLocation(this.locationId).subscribe(res => {
					this.initForm(res.data);
				})
			}
			else
				this.initForm();
		})
	}

 	getDropdownData(): void {
		this.reference.getLocationType().subscribe(res => this.types = res.data);
	}

  	hasErrors(field): boolean {
		return this.locationForm.touched && this.locationForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.locationForm.get(field).touched && 
			this.locationForm.get(field).hasError(error)
		);
	}

  	initForm(location?: any): void {
		if (location) {
			this.locationForm = this.formBuilder.group({
				id: this.locationId,
				name: this.formBuilder.control((location && location.name) || null, [Validators.required]),
				address: this.formBuilder.control((location && location.address) || null, [Validators.required]),
				type: this.formBuilder.control((location && !isNaN(location.type) ? location.type : null), [Validators.required]),
				places: this.formBuilder.control((location && location.places) || null, [Validators.required, Validators.pattern('/^[1-9]\d*$/'),Validators.min(1)]),
				description: this.formBuilder.control((location && location.description) || null, [Validators.required]),
			});
			this.isLoading = false;
		}
		else {
			this.locationForm = this.formBuilder.group({
				name: this.formBuilder.control(null, [Validators.required]),
				address: this.formBuilder.control(null, [Validators.required]),
				type: this.formBuilder.control(0, [Validators.required]),
				places: this.formBuilder.control(0, [Validators.required]),
				description: this.formBuilder.control(null, [Validators.required])
			});
			this.isLoading = false;
		}
	}

  	onSave(): void {
		if (this.locationId) {
			this.editLocation();
		} else {
			this.addLocation();
		}
	}

 	 backClicked() {
		this.location.back();
	}

  	addLocation(): void {
		this.locationService.createLocation({data: this.locationForm.value}).subscribe(() => {
			this.backClicked();
			this.notificationService.success('Success', 'Location was successfully added', NotificationUtil.getDefaultMidConfig());
		});
	}

 	editLocation(): void {
		this.locationService.editLocation({data: this.locationForm.value}).subscribe(() => {
			this.backClicked();
			this.notificationService.success('Success', 'Location was successfully updated', NotificationUtil.getDefaultMidConfig());
		});
	}
}
