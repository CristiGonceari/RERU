import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventTestTemplateService } from '../../../utils/services/event-test-template/event-test-template.service';
import { MedicalColumnEnum } from '../../../utils/enums/medical-column.enum';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { CandidatePositionNotificationService } from '../../../utils/services/candidate-position-notifications/candidate-position-notification.service'

@Component({
	selector: 'app-add-edit-position',
	templateUrl: './add-edit-position.component.html',
	styleUrls: ['./add-edit-position.component.scss']
})
export class AddEditPositionComponent implements OnInit {
	isLoading: boolean = true;
	isEdit: boolean = true;
	positionId: number;
	positionForm: FormGroup;
	positionName: string;
	title: string;
	placeHolderStringRequireDocuments = '+ Document';
	placeHolderStringRequireEvents = '+ Eveniment';
	items = [];
	eventList = [];
	eventSelected: any[] = [];
	tags: any[] = [];
	description: string;
	event = new SelectItem();
	eventsList: any[] = [];
	exceptUserIds = [];
	attachedUsers: any[] = [];
	editorData: string = '';
	userListToAdd: [] = [];

	startDate;
	endDate;
	fromDate;
	tillDate;
	showEventCard: boolean = false;
	eventsTagsList = [];
	eventsListForDescription = [];
	medicalColumsList: SelectItem[] = [{ label: '', value: '' }];

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
		private router: Router,
		private positionService: CandidatePositionService,
		private location: Location,
		private referenceService: ReferenceService,
		private eventTestTemplateService: EventTestTemplateService,
		private modalService: NgbModal,
		private candidatePositionNotificationService: CandidatePositionNotificationService
	) { }

	ngOnInit(): void {
		this.positionForm = new FormGroup({
			name: new FormControl(),
			isActive: new FormControl(),
			medicalColumn: new FormControl()
		});
		this.onTextChange("");
		this.getEvents();
		this.getMedicalColumnsEnum();
		this.route.params.subscribe(params => {
			if (params.id) {
				this.isEdit = true;
				this.positionId = params.id;
				this.positionService.get(this.positionId).subscribe(res => {
					this.initForm(res.data);
					this.editorData = res.data.description;
					this.startDate = res.data.from;
					this.endDate = res.data.to;

					res.data.requiredDocuments.forEach(element => {
						this.tags.push({ display: element.label, value: +element.value })
					});

					res.data.events.forEach(el => {
						this.eventSelected.push({ display: el.label, value: +el.value })
						this.eventsTagsList.push({ display: el.label, value: +el.value })
					})

					this.getTestTemplateByEventsIds(this.eventSelected);
					if(this.eventSelected.length > 0) this.showEventCard = true;
				})
				this.isLoading = false;
			} else {
				this.isEdit = false;
				this.initForm();
				this.isLoading = false;
			}
		});
	}

	getMedicalColumnsEnum() {
		this.referenceService.getMedicalColumnEnum().subscribe(res => this.medicalColumsList = res.data);
	}

	onAddEvent(event) {
		this.eventsTagsList.push(event);
		this.showEventCard = true;
		this.getTestTemplateByEventsIds(this.eventsTagsList);
	}

	onRemovingEvent(event) {
		const index = this.eventsTagsList.indexOf(event);
		if (index !== -1) {
			this.eventsTagsList.splice(index, 1);
		}

		if (this.eventsTagsList.length < 1) this.showEventCard = false;
		this.getTestTemplateByEventsIds(this.eventsTagsList);
	}

	getTestTemplateByEventsIds(eventsTagsList) {
		this.eventTestTemplateService.getTestTemplateByEventsIds({ eventIds: eventsTagsList.map(x => x.value) }).subscribe(res => {
			this.eventsListForDescription = res.data;
		})
	}

	setTimeToSearch(): void {
		if (this.startDate) {
			const date = new Date(this.startDate);
			this.fromDate = new Date(date.getTime() - (new Date(this.startDate).getTimezoneOffset() * 60000)).toISOString();
		}
		if (this.endDate) {
			const date = new Date(this.endDate);
			this.tillDate = new Date(date.getTime() - (new Date(this.endDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	initForm(data?): void {
		this.getAttachedUsers();
		if (data) {
			this.positionForm = this.fb.group({
				name: this.fb.control(data?.name || null, [Validators.required]),
				isActive: this.fb.control(data?.isActive || false),
				medicalColumn: this.fb.control((data && !isNaN(data.medicalColumn) ? data.medicalColumn : null), [Validators.required])
			});
		} else {
			this.positionForm = this.fb.group({
				name: this.fb.control(null, [Validators.required]),
				isActive: this.fb.control(false, [Validators.required]),
				medicalColumn: this.fb.control(null, [Validators.required]),
			});
		}

		if (!this.isEdit && this.attachedUsers.length <= 0) {
			this.candidatePositionNotificationService.getMyId().subscribe(res => {
				this.attachedUsers.push(+res.data)
			})
		}
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

	getAttachedUsers() {
		this.candidatePositionNotificationService.getUserIds(this.positionId).subscribe(res => this.attachedUsers = res.data)
	}

	attachUsers() {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.exceptUserIds = [];
		modalRef.componentInstance.page = this.router.url.split("/").pop();;
		modalRef.componentInstance.attachedItems = this.attachedUsers;
		modalRef.componentInstance.inputType = 'checkbox';
		modalRef.result.then(() => {
			this.attachedUsers = modalRef.result.__zone_symbol__value.attachedItems;
		}, () => { });
	}

	addRole(): void {
		this.setTimeToSearch();

		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
		const eventArr = [];

		this.eventSelected.forEach(x => {
			if (typeof x.value !== 'string') eventArr.push(x);
		})

		let addPositionModel = {
			name: this.positionForm.value.name,
			isActive: this.positionForm.value.isActive,
			from: this.fromDate,
			to: this.tillDate,
			description: this.editorData,
			medicalColumn: !isNaN(this.positionForm.value.medicalColumn) ? this.positionForm.value.medicalColumn : null,
			requiredDocuments: tagsArr,
			eventIds: eventArr.map(obj => obj.value),
			userProfileIds: this.attachedUsers
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
		this.setTimeToSearch();

		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
		const eventArr = [];

		this.eventSelected.forEach(x => {
			if (typeof x.value !== 'string') eventArr.push(x);
		})

		let editPositionModel = {
			id: +this.positionId,
			name: this.positionForm.value.name,
			from: this.fromDate,
			to: this.tillDate,
			description: this.editorData,
			medicalColumn: !isNaN(this.positionForm.value.medicalColumn) ? this.positionForm.value.medicalColumn : null,
			isActive: this.positionForm.value.isActive,
			requiredDocuments: tagsArr,
			eventIds: eventArr.map(obj => obj.value),
			userProfileIds: this.attachedUsers
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
