import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n.service';
import { CandidatePositionService } from '../../../utils/services/candidate-position.service'
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { CandidatePositionModel } from '../../../utils/models/candidate-position.model';

@Component({
	selector: 'app-add-edit-position',
	templateUrl: './add-edit-position.component.html',
	styleUrls: ['./add-edit-position.component.scss']
})
export class AddEditPositionComponent implements OnInit {
	isLoading: boolean = true;
	positionId: number;
	positionForm: FormGroup;
	positionName: string;
	title: string;
	description: string;

	constructor(
		private fb: FormBuilder,
		private notificationService: NotificationsService,
		public translate: I18nService,
		private route: ActivatedRoute,
		private positionService: CandidatePositionService,
		private location: Location
	) { }

	ngOnInit(): void {
		this.positionForm = new FormGroup({
			name: new FormControl(),
			isActive: new FormControl()
		});
		this.route.params.subscribe(params => {
			if (params.id) {
				this.positionId = params.id;
				this.positionService.get(this.positionId).subscribe(res => {
					this.initForm(res.data);
				})
				this.isLoading = false;
			} else {
				this.initForm();
				this.isLoading = false;
			}
		});
	}

	initForm(data?): void {
		this.positionForm = this.fb.group({
			name: this.fb.control(data?.name || null, [Validators.required]),
			isActive: this.fb.control(data?.isActive || false)
		});
	}

	addRole(): void {
		let addPositionModel = {
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive,
		} as CandidatePositionModel;
		this.positionService.create(addPositionModel).subscribe(
			() => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('pages.positions.success-create'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.back();
			}
		);
	}

	editRole(): void {
		let editPositionModel = {
			id: +this.positionId,
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive
		} as CandidatePositionModel;
		this.positionService.editPosition(editPositionModel).subscribe(
			() => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('pages.positions.success-update'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.back();
			}
		);
	}

	submit(): void {
		if (this.positionId) {
			this.editRole();
		} else {
			this.addRole();
		}
	}

	back(): void {
		this.location.back();
	}
}
