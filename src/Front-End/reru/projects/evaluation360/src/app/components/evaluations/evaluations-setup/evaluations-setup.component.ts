import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SelectItem } from '../../../utils/models/select-item.model';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EvaluationService } from '../../../utils/services/evaluations.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AttachUserModalComponent, AttachUserModel } from '../../../utils/modals/attach-user-modal/attach-user-modal.component';

@Component({
	selector: 'app-evaluations-setup',
	templateUrl: './evaluations-setup.component.html',
	styleUrls: ['./evaluations-setup.component.scss'],
})
export class EvaluationsSetupComponent implements OnInit {
	evaluationForm: FormGroup;
	counterSignUsers: SelectItem[] = [];
	evaluatedUsers: SelectItem[] = [];

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

	openAttachUserModal(isAttachEvaluated: boolean = false): void {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.inputType = isAttachEvaluated ? 'checkbox' : 'radio';
		modalRef.result.then((data: AttachUserModel) => {
			if (isAttachEvaluated) {
				this.evaluationForm.get('evaluatedUserProfileIds').patchValue(data.selectedUsers)
			} else {
				this.evaluationForm.get('counterSignerUserProfileId').patchValue(data.selectedUsers[0])
			}
		}, () => { });
	}

	initForm(): void {
		this.evaluationForm = this.fb.group({
			evaluatedUserProfileIds: this.fb.control(null, [Validators.required]),
			counterSignerUserProfileId: this.fb.control(null, [Validators.required]),
			type: this.fb.control(null, [Validators.required]),
		});
	}

	submit(): void {
		const data = {
			...this.evaluationForm.value,
			type: +this.evaluationForm.value.type
		}
		this.evaluationService.create(data).subscribe(() => {
				this.notificationService.success(
					'Success',
					'S-a initializat procesul de evaluare cu succes!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.ngZone.run(() => this.router.navigate(['../', 'list'], { relativeTo: this.route }));
			}, (error) => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error('Error', 'A server occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}
}
