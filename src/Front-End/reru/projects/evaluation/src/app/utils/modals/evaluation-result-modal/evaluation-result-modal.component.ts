import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TestResultStatusEnum } from '../../enums/test-result-status.enum';
import { TestService } from '../../services/test/test.service';
import { QualifyingTypeEnum } from '../../enums/qualifying-type.enum';
import { TestTemplateService } from '../../services/test-template/test-template.service';
import { __values } from 'tslib';

@Component({
  selector: 'app-evaluation-result-modal',
  templateUrl: './evaluation-result-modal.component.html',
  styleUrls: ['./evaluation-result-modal.component.scss']
})
export class EvaluationResultModalComponent implements OnInit {
  testId;
  testTemplateId;
  qualificationTypeEnum =  QualifyingTypeEnum;
  qualificationTypeValue: number;

  enum = TestResultStatusEnum;

  recomdendedListOfValues = [
    {
      id: 1,
      title: 'C1',
      checked: false,
      disabled: false
    },
    {
      id: 2,
      title: 'C2',
      checked: false,
      disabled: false
    },
    {
      id: 3,
      title: 'C3',
      checked: false,
      disabled: false
    },
    {
      id: 4,
      title: 'C4',
      checked: false,
      disabled: false
    },
  ];

  recomendedValues;
  notRecomendedValues;
  masterSelected: boolean;


  constructor(
    private activeModal: NgbActiveModal, 
    private testService: TestService, 
    private router: Router,
    private testTemplateService: TestTemplateService
    ) { }

  ngOnInit(): void {
    this.getTestTemplate(this.testTemplateId);
  }

  getTestTemplate(id: any){
    this.testTemplateService.getTestTemplate(id).subscribe(res => {
      this.qualificationTypeValue = res.data.qualifyingType;
    })
  }
 
  isAllSelected() {
    this.masterSelected = this.recomdendedListOfValues.every(function (item: any) {
      return item.checked == true;
    })
    this.getCheckedItemForRecomendedValues();
  }

  getCheckedItemForRecomendedValues() {
    this.recomendedValues = [];
    for (var i = 0; i < this.recomdendedListOfValues.length; i++) {
      if (this.recomdendedListOfValues[i].checked){
          this.recomendedValues.push(this.recomdendedListOfValues[i]);
        }
    }
    // this.checkButtonToDisable();
    this.getCheckedItemForNotRecomendedValues();
  }

  // checkButtonToDisable(){
  //   for (var i = 0; i < this.recomdendedListOfValues.length; i++) {
  //     if(!this.recomdendedListOfValues[i].checked && this.recomendedValues.length == 2){
  //         this.recomdendedListOfValues[i].disabled = true;
  //     }else if(!this.recomdendedListOfValues[i].checked && this.recomdendedListOfValues[i].disabled){
  //       this.recomdendedListOfValues[i].disabled = false;
  //     }
  //   }
  // }

  getCheckedItemForNotRecomendedValues(){
    // if(this.recomendedValues.length == 2){
      let arr = [];
      for(let i = 0; i < this.recomendedValues.length; i ++){
        if(i == 0){
          arr = this.recomdendedListOfValues.filter(f => f.id != this.recomendedValues[i].id);
        }else if (i >= 1){
          arr = arr.filter(f => f.id != this.recomendedValues[i].id);
        }
      }
      this.notRecomendedValues = arr;
    // }
  }

  close(): void {
    this.activeModal.close();
  }

  setStatus(status, recomendedValues?, notRecomendedValues?){
    if(recomendedValues != null){
      recomendedValues = recomendedValues.map(function(obj) {
      return obj.id;
      });
    }
    if(notRecomendedValues != null){
      notRecomendedValues = notRecomendedValues.map(function(obj) {
        return obj.id;
        });
    }
    
    let data = {
      testId: this.testId,
      resultStatus: status,
      recommendedFor: recomendedValues || null,
      notRecommendedFor: notRecomendedValues || null
    }
    this.testService.setResult(data).subscribe(() => {this.close(); this.finalizeTest()});
  }

  finalizeTest() {
    this.testService.finalizeEvaluation(this.testId).subscribe(() => this.router.navigate(['my-activities/my-evaluations']));
  }
}
