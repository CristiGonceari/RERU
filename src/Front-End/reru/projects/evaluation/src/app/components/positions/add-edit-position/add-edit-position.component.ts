import { Component, OnInit, ViewChild } from '@angular/core';
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
import { SelectItem } from '../../../utils/models/select-item.model';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
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
	placeHolderString = '+ Tag';
	items = [];
	eventList = [];
	eventSelected: any[] = [];
	tags: any[] = [];
	description: string;
	event = new SelectItem();
	eventsList: any[] = [];
	exceptUserIds = [];
	editorData: string = '';

	public Editor = DecoupledEditor;
	public onReady(editor) {
		editor.ui.getEditableElement().parentElement.insertBefore(
			editor.ui.view.toolbar.element,
			editor.ui.getEditableElement()
		);
	}

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
		this.getEvents();
		this.route.params.subscribe(params => {
			if (params.id) {
				this.positionId = params.id;
				this.positionService.get(this.positionId).subscribe(res => {
					this.initForm(res.data);
					this.editorData = res.data.description;

					res.data.requiredDocuments.forEach(element => {
						this.tags.push({ display: element.label, value: +element.value })
					});

					res.data.events.forEach(el => {
						this.eventSelected.push({ display: el.label, value: +el.value })
					})
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

	getEvents() {
		this.referenceService.getEvents().subscribe(res => {
			if (res && res.data) {
				res.data.forEach(el => {
					this.eventList.push({ display: el.eventName, value: +el.eventId });
				});
			}
		})
	}

	addRole(): void {
		this.isLoading = true;

		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
		const eventArr = [];

		this.eventSelected.forEach(x => {
			if (typeof x.value !== 'string') eventArr.push(x);
		})

		let addPositionModel = {
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive,
			description: this.editorData,
			requiredDocuments: tagsArr,
			eventIds: eventArr.map(obj => obj.value)
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
		const eventArr = [];

		this.eventSelected.forEach(x => {
			if (typeof x.value !== 'string') eventArr.push(x);
		})

		let editPositionModel = {
			id: +this.positionId,
			name: this.positionForm.value.name,
			description: this.editorData,
			isActive: this.positionForm.value.isActive,
			requiredDocuments: tagsArr,
			eventIds: eventArr.map(obj => obj.value)
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
