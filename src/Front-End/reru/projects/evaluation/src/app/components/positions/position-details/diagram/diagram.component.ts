import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { CandidatePositionService } from '../../../../utils/services/candidate-position/candidate-position.service';
import { PrintTemplateService } from '../../../../utils/services/print-template/print-template.service';
import { saveAs } from 'file-saver';
import { MedicalColumnEnum } from '../../../../utils/enums/medical-column.enum';
import { EnumStringTranslatorService } from '../../../../utils/services/enum-string-translator.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewPositionDiagramModalComponent } from 'projects/evaluation/src/app/utils/modals/view-position-diagram-modal/view-position-diagram-modal.component';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../../utils/services/i18n/i18n.service';
import { NotificationUtil } from '../../../../utils/util/notification.util';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { NotificationsService } from 'angular2-notifications';

@Component({
	selector: 'app-diagram',
	templateUrl: './diagram.component.html',
	styleUrls: ['./diagram.component.scss']
})

export class DiagramComponent implements OnInit {
	isLoading = true;
	eventsDiagram = [];
	usersDiagram = [];
	testTemplates = [];
	positionId;
	positionName;
	positionMedicalColumn;
	events = [];
	medicalColumnEnum = MedicalColumnEnum;
	status = TestStatusEnum;
	result = TestResultStatusEnum;

	fileName: string;
	fileStatus = { requestType: '', percent: 1 }
	isLoadingMedia: boolean = false;
	title: string;
	description: string;
	no: string;
	yes: string;

	isOpenAddTest: boolean = false;
	isOpenAddEvaluation: boolean = false;

	selectedEventId;
	selectedTestTemplateId;

	constructor(
		private positionService: CandidatePositionService,
		private activatedRoute: ActivatedRoute,
		private printService: PrintTemplateService,
		private enumStringTranslatorService: EnumStringTranslatorService,
		public modalService: NgbModal,
		public translate: I18nService,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.activatedRoute.parent.params.subscribe(params => {
			this.positionId = params.id;
			this.positionService.get(this.positionId).subscribe(res => {
				this.positionName = res.data.name;
				this.positionMedicalColumn = res.data.medicalColumn;
				this.events = res.data.events;

				if (this.events.length) this.getDiagram();
				else this.isLoading = false;
			});
		});
	}

	getDiagram(): void {
		this.isLoading = true;

		this.positionService.getDiagram({ positionId: this.positionId }).subscribe(res => {
			if (res && res.data) {
				this.eventsDiagram = res.data.eventsDiagram;
				this.usersDiagram = res.data.usersDiagram;

				this.eventsDiagram.forEach(event => {
					event.testTemplates.forEach(element => {
						this.testTemplates.push(element)
					});
				});
				this.isLoading = false;
			}
		})
	}

	translateResultValue(item) {
		return this.enumStringTranslatorService.translateTestResultValue(item);
	}

	printPositionDiagram() {
		this.isLoadingMedia = true;

		this.printService.getPositionDiagramPdf(this.positionId).subscribe((response: any) => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('position.success-download'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.reportProggress(response, true);
		},
			(error) => {
				forkJoin([
					this.translate.get('modal.error'),
					this.translate.get('position.error-download'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.isLoadingMedia = false;
			});
	}

	getPositionDiagramExcel() {
		this.isLoadingMedia = true;

		const params = {
			positionId: this.positionId
		}

		this.positionService.exportPositionDiagram(params).subscribe((event) => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('position.success-download'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.reportProggress(event, false);
		},
			(error) => {
				forkJoin([
					this.translate.get('modal.error'),
					this.translate.get('position.error-download'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.isLoadingMedia = false;
			})
	}

	private reportProggress(httpEvent: HttpEvent<Blob>, isPdf): void {
		let fileName;

		switch (httpEvent.type) {
			case HttpEventType.Sent:
				this.fileStatus.percent = 1;
				break;
			case HttpEventType.UploadProgress:
				this.updateStatus(httpEvent.loaded, httpEvent.total, 'În progres...')
				break;
			case HttpEventType.DownloadProgress:
				this.updateStatus(httpEvent.loaded, httpEvent.total, 'În progres...')
				break;
			case HttpEventType.Response:
				this.fileStatus.requestType = "Terminat";
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());

				if (isPdf) {
					fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

					if (httpEvent.body.type === 'application/pdf') {
						fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
					}
				} else {
					fileName = httpEvent.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				}

				const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
				const file = new File([blob], fileName, { type: httpEvent.body.type });
				saveAs(file);
				this.isLoadingMedia = false;
				break;
		}
	}

	updateStatus(loaded: number, total: number | undefined, requestType: string) {
		this.fileStatus.requestType = requestType;
		this.fileStatus.percent = Math.round(100 * loaded / total);
	}

	openFullScreenMode() {
		const modalRef: any = this.modalService.open(ViewPositionDiagramModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.eventsDiagram = this.eventsDiagram;
		modalRef.componentInstance.usersDiagram = this.usersDiagram;
		modalRef.componentInstance.testTemplates = this.testTemplates;
		modalRef.componentInstance.positionId = this.positionId;
		modalRef.result.then(() => {
			if (((modalRef.result?.__zone_symbol__value?.isOpenAddTest ||
				modalRef.result?.__zone_symbol__value?.isOpenAddEvaluation) &&
				modalRef.result?.__zone_symbol__value?.selectedEventId &&
				modalRef.result?.__zone_symbol__value?.selectedTestTemplateId) != null
			) {
				this.isOpenAddTest = modalRef.result.__zone_symbol__value.isOpenAddTest;
				this.isOpenAddEvaluation = modalRef.result.__zone_symbol__value.isOpenAddEvaluation;
				this.selectedEventId = modalRef.result.__zone_symbol__value.selectedEventId;
				this.selectedTestTemplateId = modalRef.result.__zone_symbol__value.selectedTestTemplateId;
			}
		}, () => { })
	}

	openAddTest(value) {
		if (value.mode == 0) {
			this.isOpenAddTest = true;
			this.selectedEventId = value.eventId;
			this.selectedTestTemplateId = value.testTemplateId;
		} else if (value.mode == 2) {
			this.isOpenAddEvaluation = true;
			this.selectedEventId = value.eventId;
			this.selectedTestTemplateId = value.testTemplateId;
		}
	}

	onChangeAddTest(value: boolean) {
		this.isOpenAddTest = value;
		this.clearDiagramData();
		this.getDiagram();
	}

	onChangeAddEvaluation(value: boolean) {
		this.isOpenAddEvaluation = value;
		this.clearDiagramData();
		this.getDiagram();
	}

	clearDiagramData() {
		this.eventsDiagram = [];
		this.usersDiagram = [];
		this.testTemplates = [];
	}
}
