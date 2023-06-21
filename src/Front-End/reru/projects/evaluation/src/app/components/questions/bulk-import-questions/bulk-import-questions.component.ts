import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { QuestionService } from '../../../utils/services/question/question.service';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-bulk-import-questions',
  templateUrl: './bulk-import-questions.component.html',
  styleUrls: ['./bulk-import-questions.component.scss']
})
export class BulkImportQuestionsComponent implements OnInit {
  	selectedTypeName: string = QuestionUnitTypeEnum[1];
	selectedTypeId = 1;
	questionTypes: string[];
	files: File[] = [];
	fileStatus = { requestType: '', percent: 1 }
	fileUploadQueue = [];
	isLoading: boolean = false;
	isLoadingMedia: boolean = false;
	title: string;
  	description: string;

	constructor(
		public activeModal: NgbActiveModal,
		private questionService: QuestionService,
		public translate: I18nService,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.questionTypes = Object.keys(QuestionUnitTypeEnum)
			.map(key => QuestionUnitTypeEnum[key])
			.filter(value => typeof value === 'string') as string[];
	}

	selectType(val) {
		this.selectedTypeId = +QuestionUnitTypeEnum[val];
	}

	downloadTemplate(selectedType) {
		var fileName; 
		
		forkJoin([
			this.translate.get('questions.add-one-answer'),
			this.translate.get('questions.add-multiple-answers'),
			this.translate.get('questions.add-file-answer'),
			this.translate.get('questions.add-free-text'),
			this.translate.get('questions.add-hashed-answer'),
		  ]).subscribe(([oneAnswer, multipleAnswer, fileAnswer, freeText, hashedAnswer]) => {
			switch(selectedType) {
				case 1: fileName = freeText;
					break;
				case 2: fileName = multipleAnswer;
					break;
				case 3: fileName = oneAnswer;
					break;
				case 4: fileName = hashedAnswer;
					break;
				case 5: fileName = fileAnswer;
					break;
			}
		  });
		const fileType = 'application/vnd.ms.excel';
		this.questionService.getTemplate(selectedType).subscribe(
			res => {
				this.isLoadingMedia = true;
				this.fileStatus.percent = 1;
				const blob = new Blob([res.body], { type: fileType });
				const file = new File([blob], fileName, { type: fileType });
				saveAs(file);
				this.reportProggress(res);
			},
			() => {
				console.error('error in getting template');
			},
		);
	}

	onSelect(event) {
		this.files.push(...event.addedFiles);
	}

	onRemove(event) {
		this.files.splice(this.files.indexOf(event), 1);
	}

	onConfirm(): void {
		this.isLoading = true;
		const formData = new FormData();
		formData.append('file', this.files[0]);
		forkJoin([
			this.translate.get('modal.success'),
			this.translate.get('questions.succes-add-msg'),
		]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
		});
		this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		this.questionService.bulkUpload(formData).subscribe(
			(res) => {
				const blob = new Blob([res.body], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
				this.questionService.uploadQuestions.next();
				this.isLoading = false;
				if(res.body.size > 0) {
					const file = new File([blob], 'error.xlsx', { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
					saveAs(file);
					alert("Somethig wrong! Please check your .xlsx file.");
					this.files = [];
				}
				else this.activeModal.close();
			}, (error) => {
				forkJoin([
					this.translate.get('notification.title.error'),
					this.translate.get('notification.body.error'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	dismiss(){
		this.activeModal.close();
	}

	private reportProggress(httpEvent: HttpEvent<string[] | Blob>) {	
		switch (httpEvent.type) {
		  case HttpEventType.Sent:
			this.fileStatus.percent = 1;
			break;
		  case HttpEventType.UploadProgress:
			this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
			break;
		  case HttpEventType.DownloadProgress:
			this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
			break;
		  case HttpEventType.Response:
			this.fileStatus.requestType = "Done";
			this.fileStatus.percent = 100;	
			setTimeout(() => { this.isLoadingMedia = false; }, 1000);
			break;
		}
	}

	updateStatus(loaded: number, total: number | undefined, requestType: string, index?: number) {
		this.fileStatus.requestType = requestType;
	
		const foundIndex = this.fileUploadQueue.findIndex(x => x.fileIndex == index);
		this.fileUploadQueue[foundIndex].percent =  this.fileUploadQueue[foundIndex].percent + ((Math.round(90 * loaded / total) / this.files.length) - this.fileUploadQueue[foundIndex].percent );
		
		let totalValue: number = 0;
		for(const value of this.fileUploadQueue){
		  totalValue += value.percent;
		}
	
		this.fileStatus.percent = Math.round((this.fileStatus.percent + (totalValue - this.fileStatus.percent)));
	}
}
