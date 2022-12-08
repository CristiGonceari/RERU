import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { EvaluationModel } from '../../models/evaluation.model';
import { createEvaluatorForm, 
         createEvaluatedForm, 
         createCounterSignForm,
         isInvalidPattern,
         isValid, 
} from '../../util/forms.util';
import { parseEvaluatedModel, parseCounterSignModel } from '../../util/parsings.util';
import { EvaluationRoleEnum } from '../../models/evaluation-role.enum';
import { EvaluationAcceptModel } from '../../models/evaluation-accept.model';
import { EvaluationCounterSignModel } from '../../models/evaluation-countersign.model';
import { EvaluationTypeEnum } from '../../models/type.enum';
import { Moment } from 'moment';
import * as moment from 'moment';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  isLoading: boolean = true;

  evaluationForm: FormGroup;
  evaluatedForm: FormGroup;
  counterSignForm: FormGroup;
  evaluatedKnowForm: FormGroup;

  EvaluatinRoleEnum = EvaluationRoleEnum;
  EvaluationTypeEnum = EvaluationTypeEnum;

  @ViewChild('finalEvalNum') finalEvalNum: ElementRef;
  @Input() evaluation: EvaluationModel;
  @Input() evaluationRole: EvaluationRoleEnum;
  @Output() request: EventEmitter<EvaluationModel|EvaluationAcceptModel|EvaluationCounterSignModel> = 
  new EventEmitter<EvaluationModel|EvaluationAcceptModel|EvaluationCounterSignModel>();

  parsedDateCompletionGeneralData: string | Date = '';
  parsedPeriodEvaluatedFromTo: string | Date = '';
  parsedPeriodEvaluatedUpTo: string | Date = '';
  parsedPeriodRunningActivityFromTo: string | Date = '';
  parsedPeriodRunningActivityUpTo: string | Date = '';
  parsedPartialEvaluationPeriodFromTo: string | Date = '';
  parsedPartialEvaluationPeriodUpTo: string | Date = '';
  parsedDateSanctionApplication: string | Date = '';
  parsedDateLiftingSanction: string | Date = '';
  parsedAppointmentDate: string | Date = '';
  parsedDateEvaluatiorInterview: string | Date = '';
  parsedDateSettingIindividualGoals: string | Date = '';

  questions: number[] = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18];
  
  isInvalidPattern: Function;
  isValid: Function;
  constructor() {
    this.isInvalidPattern = isInvalidPattern.bind(this);
    this.isValid = isValid.bind(this);
   }

  get isEvaluatorRoleView(): boolean {
    return [EvaluationRoleEnum.Evaluator,
            EvaluationRoleEnum.Evaluated,
            EvaluationRoleEnum.CounterSigner,
            EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)
  }

  set isEvaluatorRoleView(value: boolean) {};

  get isEvaluatedRoleView(): boolean {
    return [EvaluationRoleEnum.Evaluated].includes(this.evaluationRole)
  }

  set isEvaluatedRoleView(value: boolean) {};

  get isCounterSignerView(): boolean {
    return [EvaluationRoleEnum.CounterSigner,
            EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)
  }

  set isCounterSignerView(value: boolean) {};

  get isEvaluatedKnowView(): boolean {
    return [EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)
  }

  set isEvaluatedKnowView(value: boolean) {};

  get isDisabledEvaluator(): boolean {
    return this.evaluationRole != EvaluationRoleEnum.Evaluator;
  }

  get isDisabledEvaluated(): boolean {
    return this.evaluationRole != EvaluationRoleEnum.Evaluated;
  }

  get isDisabledCounterSigner(): boolean {
    return this.evaluationRole != EvaluationRoleEnum.CounterSigner;
  }

  ngOnInit(): void {
    this.initForm(this.evaluation);
    this.assignDates(this.evaluation);
  }

  initForm(data: EvaluationModel): void {
    switch(this.evaluationRole) {
      case EvaluationRoleEnum.Evaluator:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(data), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(data), this.evaluationRole);
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.Evaluated:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(data), this.evaluationRole);
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.CounterSigner:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(data), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(data), this.evaluationRole);
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.EvaluatedKnow:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(data), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(data), this.evaluationRole);
        this.isLoading = false;
        break;
    }
  }

  submit(isConfirm?): void {

  }

  parseDate(value: Moment, formControlName: string, form: FormGroup): void {
    if (!Date.parse(value as any)) {
      return;
    }

    const date = moment(value).toDate();
    form.get(formControlName).patchValue(new Date(date).toISOString());
  }

  assignDates(data: EvaluationModel): void {
    this.parsedDateCompletionGeneralData = data.dateCompletionGeneralData;
    this.parsedPeriodEvaluatedFromTo = data.periodEvaluatedFromTo; 
    this.parsedPeriodEvaluatedUpTo = data.periodEvaluatedUpTo; 
    this.parsedPeriodRunningActivityFromTo = data.periodRunningActivityFromTo; 
    this.parsedPeriodRunningActivityUpTo = data.periodEvaluatedUpTo; 
    this.parsedPartialEvaluationPeriodFromTo = data.partialEvaluationPeriodFromTo; 
    this.parsedPartialEvaluationPeriodUpTo = data.partialEvaluationPeriodUpTo; 
    this.parsedDateSanctionApplication = data.dateSanctionApplication; 
    this.parsedDateLiftingSanction = data.dateLiftingSanction; 
    this.parsedAppointmentDate = data.appointmentDate; 
    this.parsedDateEvaluatiorInterview = data.dateEvaluatiorInterview; 
    this.parsedDateSettingIindividualGoals = data.dateSettingIindividualGoals;
  }

  handleFinalQualificationChange(value: number, isInputChange: boolean = false): void {
    if (isInputChange) {
      switch(true) {
        case value >= 3.51 && value <= 4.00: this.evaluationForm.get('finalEvaluationQualification').patchValue('4');break;
        case value >= 2.51 && value <= 3.50: this.evaluationForm.get('finalEvaluationQualification').patchValue('3');break;
        case value >= 1.51 && value <= 2.50: this.evaluationForm.get('finalEvaluationQualification').patchValue('2');break;
        case value >= 1.00 && value <= 1.50: this.evaluationForm.get('finalEvaluationQualification').patchValue('1');break;
        default: this.evaluationForm.get('finalEvaluationQualification').patchValue(null);break;
      }
      return
    }

    this.finalEvalNum.nativeElement.value = +value;
  }

  /**
   * Calculating M1 - index 6, M2 - index 10 and M3 - indx 12
   * @returns {number} Media (M1, M2, Pb)
   * isFixed = defaults true for UI
   */
  calculateAverageCriterias(index: number, isFixed: boolean = true): number {
    if (isFixed) {
      switch(index) {
        case 6: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc + (+this.evaluationForm.get(`question${i+1}`)?.value || 0), 0) / 5).toFixed(2);
        case 10: return +(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+6}`)?.value || 0), 0.00) / 3).toFixed(2);
        case 12: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`score${i + 1}`)?.value || 0), 0.00) / 3).toFixed(2);
        case 17: return +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+9}`)?.value || 0), 0.00) / 4).toFixed(2);
      }
    }

    switch(index) {
      case 6: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc + (+this.evaluationForm.get(`question${i+1}`)?.value || 0), 0) / 5)
      case 10: return +(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+6}`)?.value || 0), 0.00) / 3)
      case 12: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`score${i + 1}`)?.value || 0), 0.00) / 3)
      case 17: return +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+9}`)?.value || 0), 0.00) / 4)
    }
  }

  /**
   * 
   * @returns {number} Obiective individuale (M3)
   * isFixed = defaults true for UI
   */
  calculateM3(isFixed: boolean = true): number {
    return +(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+6}`)?.value || 0), 0.00) / 3)
  }

  /**
   * 
   * @returns {number} Pregatirea de baza (M4)
   * 
   * pb - pregatirea de baza (1-4)
   * pf - pregatirea fizica
   */
  calculateM4(isFixed: boolean = true): number {
    const pb = +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm.get(`question${i+9}`)?.value || 0), 0.00) / 4);
    const pf = this.evaluatedForm.get('question13')?.value || 0;
    return isFixed ? +((pb + pf) / 2).toFixed(2) : (pb + pf) / 2;
  }

  /**
   * Punctajul la evaluarea anuala (MEa)
   */
  calculateMea(isFixed: boolean = true): number {
    const m1 = +this.calculateAverageCriterias(6, false)
    const m2 = +this.calculateAverageCriterias(6, false)
    const m3 = this.calculateM3();
    const m4 = this.calculateM4();

    return isFixed ? +((m1 + m2 + m3 + m4) / 4).toFixed(2) : (m1 + m2 + m3 + m4) / 4; 
  }

  /**
   * 
   * @returns {number} Evaluare anuala final (Mf)
   */
  calculateMf(): number {
    const Mea = this.calculateMea();
    const Mep = this.evaluationForm.get('partialEvaluationScore')?.value || 0;
    
    return (Mea + Mep).toFixed(2); 
  }
}
