import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { QuestionService } from '../../../utils/services/question/question.service';
import { saveAs } from 'file-saver';

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
	isLoading: boolean = false;

	constructor(
		public activeModal: NgbActiveModal,
		private questionService: QuestionService
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
		const fileName = 'AddQuestionsTemplate.xlsx';
		const fileType = 'application/vnd.ms.excel';
		this.questionService.getTemplate(selectedType).subscribe(
			res => {
				const blob = new Blob([res.body], { type: fileType });
				const file = new File([blob], fileName, { type: fileType });
				saveAs(file);
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
			}
		);
	}

	dismiss(){
		this.activeModal.close();
	}
}
