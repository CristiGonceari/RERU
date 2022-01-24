import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { QuestionService } from '../../../utils/services/question/question.service';
import { saveAs } from 'file-saver';
import { OptionsService } from '../../../utils/services/options/options.service';

@Component({
  selector: 'app-bulk-import-options',
  templateUrl: './bulk-import-options.component.html',
  styleUrls: ['./bulk-import-options.component.scss']
})
export class BulkImportOptionsComponent implements OnInit {
  selectedTypeName: string = QuestionUnitTypeEnum[3];
	selectedTypeId = 3;
	qType: string[];
	hasTemplate: boolean;
	files: File[] = [];
  questionId;

	constructor(
		public activeModal: NgbActiveModal,
		private questionService: QuestionService,
    private optionService: OptionsService
	) { }

	ngOnInit(): void {
		this.hasTemplate = false;
		this.qType = Object.keys(QuestionUnitTypeEnum)
			.map(key => QuestionUnitTypeEnum[key])
			.filter(value => typeof value === 'string') as string[];
	}

	selectType(val) {
		this.selectedTypeId = +QuestionUnitTypeEnum[val];
	}

	downloadTemplate(selectedType) {
		const fileName = 'AddOptionTemplate.xlsx';
		const fileType = 'application/vnd.ms.excel';
		this.optionService.getTemplate(selectedType).subscribe(
			res => {
				const blob = new Blob([res.body], { type: fileType });
				const file = new File([blob], fileName, { type: fileType });
				saveAs(file);
			},
			() => {
				console.error('error in getting template');
			},
			() => {
				this.hasTemplate = true;
			}
		);
	}

	onSelect(event) {
		this.files.push(...event.addedFiles);
	}

	onRemove(event) {
		this.files.splice(this.files.indexOf(event), 1);
	}

	onConfirm(): void {
		const formData = new FormData();
		formData.append('Input', this.files[0]);
    formData.append('QuestionUnitId', this.questionId);
		this.optionService.bulkUpload(formData).subscribe(
			(res) => {
				const blob = new Blob([res.body], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
				this.optionService.uploadOption.next();
				
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
