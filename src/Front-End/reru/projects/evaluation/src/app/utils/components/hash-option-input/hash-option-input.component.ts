import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { TestQuestionService } from '../../services/test-question/test-question.service';

@Component({
  selector: 'app-hash-option-input',
  templateUrl: './hash-option-input.component.html',
  styleUrls: ['./hash-option-input.component.scss']
})
export class HashOptionInputComponent implements AfterViewInit{

  @ViewChild("answer") hashedAnswer;
  @Input() optionid: number;
  answerValue: string;
  isDisabled;

  constructor(public service: TestQuestionService) { }

  ngAfterViewInit(){
    this.answerValue = this.hashedAnswer.nativeElement.innerHTML;
    this.service.isDisabled.subscribe(x => this.isDisabled = x);
  }

  handleChange(answer: string) {
    this.service.answerSubject.next({ optionId: this.optionid, answer: answer });
  }
}
