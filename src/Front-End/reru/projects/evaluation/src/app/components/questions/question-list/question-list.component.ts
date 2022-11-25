 import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { QuestionByCategoryService } from '../../../utils/services/question-by-category/question-by-category.service';
import { BulkImportQuestionsComponent } from '../bulk-import-questions/bulk-import-questions.component';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.scss']
})
export class QuestionListComponent {
	title: string;
	@ViewChild('question') searchQuestion: any;
	@ViewChild('category') searchCategory: any;
	@ViewChild('type') type: any;
	@ViewChild('questionTags') questionTags: any;
	@ViewChild('status') status: any;
	
  	constructor(
		private modalService: NgbModal,
		private router: Router,
		private questionByCategory: QuestionByCategoryService
	) { }

	getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

	bulkImport(): void { 
		const modalRef = this.modalService.open(BulkImportQuestionsComponent, { centered: true, size: 'lg' });
		modalRef.result.then(
			() => { }
		);
	}

	add() {
		this.questionByCategory.setValue(false);
		this.router.navigate(['questions/add-question']);
	}

	clearFields() {
		this.searchQuestion.value='';
		this.searchCategory.value='';
		this.questionTags.value='';
		this.status.getQuestionStatus();
		this.type.getQuestionType();
	}

}
