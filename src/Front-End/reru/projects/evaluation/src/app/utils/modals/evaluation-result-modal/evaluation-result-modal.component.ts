import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TestResultStatusEnum } from '../../enums/test-result-status.enum';
import { TestService } from '../../services/test/test.service';

@Component({
  selector: 'app-evaluation-result-modal',
  templateUrl: './evaluation-result-modal.component.html',
  styleUrls: ['./evaluation-result-modal.component.scss']
})
export class EvaluationResultModalComponent implements OnInit {
  testId;
  enum = TestResultStatusEnum;
  constructor(private activeModal: NgbActiveModal, private testService: TestService, private router: Router) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }

  setStatus(status){
    let data = {
      testId: this.testId,
      resultStatus: status
    }
    this.testService.setResult(data).subscribe(() => {this.close(); this.finalizeTest()});
  }

  finalizeTest() {
    this.testService.finalizeEvaluation(this.testId).subscribe(() => this.router.navigate(['my-activities/my-evaluations']));
  }
}
