import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { CandidatePositionService } from '../../../utils/services/candidate-position/candidate-position.service'
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { CandidatePositionModel } from '../../../utils/models/candidate-position.model';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { ReferenceService } from '../../../utils/services/reference/reference.service';

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
	placeHolderString = '+ Tag'
	items = [{ display: 'Item1', value: 0 }];
	description: string;
	tags: any[] = [];



	constructor(
		private fb: FormBuilder,
		private notificationService: NotificationsService,
		public translate: I18nService,
		private route: ActivatedRoute,
		private positionService: CandidatePositionService,
		private location: Location,
		private referenceService: ReferenceService
	) { }

	ngOnInit(): void {
		this.positionForm = new FormGroup({
			name: new FormControl(),
			isActive: new FormControl()
		});

		this.onTextChange("");

		this.route.params.subscribe(params => {
			if (params.id) {
				this.positionId = params.id;
				this.positionService.get(this.positionId).subscribe(res => {
					this.initForm(res.data);
					res.data.requiredDocuments.forEach(element => {
						this.tags.push({ display: element.label, value: +element.value })
					});
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
		this.isLoading = true;

		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);

		let addPositionModel = {
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive,
			requiredDocuments: tagsArr
		} as CandidatePositionModel;
		this.positionService.create(addPositionModel).subscribe(
			() => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('position.success-create'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.back();
				this.isLoading = false;
			}
		);
	}

	editRole(): void {
		this.isLoading = true;

		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);

		let editPositionModel = {
			id: +this.positionId,
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive,
			requiredDocuments: tagsArr
		} as CandidatePositionModel;
		this.positionService.editPosition(editPositionModel).subscribe(
			() => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('position.success-update'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.back();
				this.isLoading = false;

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

	onTextChange(text: string) {
		this.referenceService.getRequiredDocumentSelectValues({ name: text }).subscribe(res => {
			res.data.forEach(element => {
				this.items.push({ display: element.label, value: +element.value })
			});
		})
	};
}
