import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SelectItem } from '../../../utils/models/select-item.model';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EvaluationService } from '../../../utils/services/evaluations.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../utils/services/i18n.service';
import { combineLatest, forkJoin, of } from 'rxjs';
import { AttachUserModalComponent } from '@erp/shared';
import { ReferenceService } from '../../../utils/services/reference.service';
import { catchError, concatMap, mergeMap } from 'rxjs/operators';
import { map } from 'rxjs/operators';

@Component({
	selector: 'app-evaluations-setup',
	templateUrl: './evaluations-setup.component.html',
	styleUrls: ['./evaluations-setup.component.scss'],
})
export class EvaluationsSetupComponent implements OnInit {
	evaluationForm: FormGroup;
	counterSignUsers: SelectItem[] = [];
	evaluatedUsers: SelectItem[] = [];
	roles: SelectItem[];
	departments: SelectItem[];
	userStatuses: SelectItem[];
	functions: SelectItem[];
	isLoading: boolean = true;
	notification = {
		title: {
			success: 'Succes',
			error: 'Error',
		},
		body: {
			create: 'S-a inițializat procesul de evaluare cu succes!',
			error: 'A avut loc o eroare!'
		}
	}
	hasCounterSigner: boolean;

	constructor(
		private readonly fb: FormBuilder,
		private readonly evaluationService: EvaluationService,
		private readonly notificationService: NotificationsService,
		private readonly ngZone: NgZone,
		private readonly router: Router,
		private readonly route: ActivatedRoute,
		private readonly modalService: NgbModal,
		private readonly translateService: I18nService,
		private readonly referenceService: ReferenceService
	) {}

	ngOnInit(): void {
		this.retrieveModalDropdowns();
		this.initForm();
		this.translateData();
		this.subscribeForTranslateChanges();
	}

	retrieveModalDropdowns(): void {
		forkJoin([
			this.referenceService.listDepartments().pipe(catchError(() => of([]))),
			this.referenceService.listRoles().pipe(catchError(() => of([]))),
			this.referenceService.listUserStatuses().pipe(
			 	concatMap(users => {
					return combineLatest([
						of(users.data),
						...users.data.map(u => this.translateService.get('user-state.'+u.label)),
					])
				}),
				map( ([ users, ...translations ]) => {	
					return (users as any[]).map( (u, i) => { u.label = translations[i]; return u;})
				}),
				catchError(() => of([]))
			),
			this.referenceService.listFunctions().pipe(catchError(() => of([])))
		]).subscribe(([departments, roles, userStatuses, functions]) => {
			this.departments = departments.data;
			this.roles = roles.data;
			this.userStatuses = userStatuses;
			this.functions = functions.data;
			this.isLoading = false;
		});
	}

	translateData(): void {
		forkJoin([
			this.translateService.get('notification.title.success'),
			this.translateService.get('message.evaluation-initialized-successfully'),
			this.translateService.get('notification.title.error'),
			this.translateService.get('notification.body.error'),
		]).subscribe(([success, create, error, bodyError ]) => {
			this.notification = { title: {success, error}, body: { create, error: bodyError }  };
		})
	}

	subscribeForTranslateChanges(): void {
		this.translateService.change.subscribe(() => {
			this.translateData();
			this.retrieveModalDropdowns();
		})
	}

	handleCounterSignChange(value: boolean): void {
		if (value) {
			this.evaluationForm.get('counterSignerUserProfileId').patchValue(null);
			this.evaluationForm.get('counterSignerUserProfileId').clearValidators();
			this.evaluationForm.get('counterSignerUserProfileId').updateValueAndValidity();
			this.counterSignUsers.length = 0;
		} else {
			this.evaluationForm.get('counterSignerUserProfileId').setValidators([Validators.required]);
			this.evaluationForm.get('counterSignerUserProfileId').updateValueAndValidity();
		}
	}

	openAttachUserModal(isAttachEvaluated: boolean = false): void {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, windowClass: 'full-size-modal' });
		modalRef.componentInstance.inputType = isAttachEvaluated ? 'checkbox' : 'radio';
		modalRef.componentInstance.departments = [...this.departments];
		modalRef.componentInstance.roles = [...this.roles];
		modalRef.componentInstance.userStatuses = [...this.userStatuses];
		modalRef.componentInstance.functions = [...this.functions];
		modalRef.componentInstance.exceptUserIds = isAttachEvaluated ? ([this.evaluationForm.get('counterSignerUserProfileId').value] || []) : this.evaluationForm?.get('evaluatedUserProfileIds')?.value || 0;
		modalRef.componentInstance.attachedUsers = isAttachEvaluated ? 
		(this.evaluationForm?.get('evaluatedUserProfileIds')?.value || []) : [this.evaluationForm.get('counterSignerUserProfileId').value] || [];
		modalRef.result.then((data) => {
			if (isAttachEvaluated) {
				this.evaluationForm.get('evaluatedUserProfileIds').patchValue([...data.selectedUsers])
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
					this.notification.title.success,
					this.notification.body.create,
					NotificationUtil.getDefaultMidConfig()
				);
				this.ngZone.run(() => this.router.navigate(['../', 'list'], { relativeTo: this.route }));
			}, (error) => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error(this.notification.title.error, this.notification.body.error, NotificationUtil.getDefaultMidConfig());
			}
		);
	}
}
