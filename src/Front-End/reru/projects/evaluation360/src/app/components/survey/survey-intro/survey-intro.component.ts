import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SelectItem } from '../../../utils/models/select-item.model';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EvaluationService } from '../../../utils/services/survey.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AttachUserModalComponent } from '../../../utils/modals/attach-user-modal/attach-user-modal.component';

@Component({
	selector: 'app-survey-intro',
	templateUrl: './survey-intro.component.html',
	styleUrls: ['./survey-intro.component.scss'],
})
export class SurveyIntroComponent implements OnInit {
	surveyForm: FormGroup;
	counterSignUsers: SelectItem[] = [];
	evaluatedUsers: SelectItem[] = [];
	userListToAdd = [];

	constructor(
		private readonly fb: FormBuilder,
		private readonly evaluationService: EvaluationService,
		private readonly notificationService: NotificationsService,
		private readonly ngZone: NgZone,
		private readonly router: Router,
		private readonly route: ActivatedRoute,
		private readonly modalService: NgbModal
	) {}

	ngOnInit(): void {
		this.initForm();
	}

	openAttachUserModal(): void {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.inputType = 'radio';
		modalRef.result.then((user: any) => {
		}, () => { });
	}

	// reloadEvaluated() {
	// 	this.setUser(null, 'evaluatedUserId');
	// 	this.referenceService.listUsers({ evaluated: true, departmentId: this.departmentId }).subscribe(response => {
	// 		this.evaluatedUsers = response;
	// 	});
	// }

	initForm(): void {
		this.surveyForm = this.fb.group({
			evaluatedUserId: this.fb.control(null, [Validators.required]),
			counterSignUserId: this.fb.control(null, [Validators.required]),
			type: this.fb.control(null, [Validators.required]),
		});
	}

	submit(): void {
		this.evaluationService.create(this.surveyForm.value).subscribe(
			response => {
				this.notificationService.success(
					'Success',
					'S-a initializat procesul de evaluare cu succes!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.ngZone.run(() => this.router.navigate(['../', 'evaluate', response], { relativeTo: this.route }));
			},
			error => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error('Error', 'A server occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}
}
