import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { EvaluationClass, EvaluationModel } from '../../models/evaluation.model';
import { createEvaluatorForm, 
         createEvaluatedForm, 
         createCounterSignForm,
         isInvalidPattern,
         isInvalidRequired,
         isValid,
         isInvalidMin,
         isInvalidMax,
         isMoreThan,
         isInvalidCustom,
         isoDateRegex, 
} from '../../util/forms.util';
import { parseEvaluatedModel, parseCounterSignModel, parseDate, generateAvgNumbers } from '../../util/parsings.util';
import { EvaluationRoleEnum } from '../../models/evaluation-role.enum';
import { EvaluationTypeEnum } from '../../models/type.enum';
import { ActionFormEnum, ActionFormModel, ActionFormType } from '../../models/action-form.type';
import { EvaluationAcceptClass } from '../../models/evaluation-accept.model';
import { EvaluationCounterSignClass } from '../../models/evaluation-countersign.model';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit, AfterViewInit {
  isLoading: boolean = true;

  evaluationForm: FormGroup;
  evaluatedForm: FormGroup;
  counterSignForm: FormGroup;
  evaluatedKnowForm: FormGroup;

  EvaluatinRoleEnum = EvaluationRoleEnum;
  EvaluationTypeEnum = EvaluationTypeEnum;
  ActionFormEnum = ActionFormEnum;

  @ViewChild('finalEvalNum') finalEvalNum: ElementRef;
  @Input() evaluation: EvaluationModel;
  @Input() evaluationRole: EvaluationRoleEnum;
  @Output() request: EventEmitter<ActionFormModel> = new EventEmitter<ActionFormModel>();

  parsedPeriodEvaluatedFromTo: string | Date = '';
  parsedPeriodEvaluatedUpTo: string | Date = '';
  parsedPeriodRunningActivityFromTo: string | Date = '';
  parsedPeriodRunningActivityUpTo: string | Date = '';
  parsedPartialEvaluationPeriodFromTo: string | Date = '';
  parsedPartialEvaluationPeriodUpTo: string | Date = '';
  parsedDateSanctionApplication: string | Date = '';
  parsedDateLiftingSanction: string | Date = '';
  parsedAppointmentDate: string | Date = '';
  parsedDateEvaluationInterview: string | Date = '';
  parsedDateSettingIindividualGoals: string | Date = '';

  questions: number[] = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18];
  
  isInvalidPattern: Function;
  isValid: Function;
  isInvalidRequired: Function;
  isInvalidMin: Function;
  isInvalidMax: Function;
  isInvalidCustom: Function;
  parseDate: Function;
  showWarnning: boolean;
  constructor() {
    this.isInvalidPattern = isInvalidPattern.bind(this);
    this.isValid = isValid.bind(this);
    this.isInvalidRequired = isInvalidRequired.bind(this);
    this.parseDate = parseDate.bind(this);
    this.isInvalidMin = isInvalidMin.bind(this);
    this.isInvalidMax = isInvalidMax.bind(this);
    this.isInvalidCustom = isInvalidCustom.bind(this);
   }

  get isEvaluatorRoleView(): boolean {
    return [EvaluationRoleEnum.Evaluator,
            EvaluationRoleEnum.Evaluated,
            EvaluationRoleEnum.CounterSigner,
            EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)
  }

  set isEvaluatorRoleView(value: boolean) {};

  get isEvaluatedRoleView(): boolean {
    return [EvaluationRoleEnum.Evaluated, 
            EvaluationRoleEnum.CounterSigner, 
            EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)
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

  get isNotCheckedCounterSigner(): boolean {
    return this.counterSignForm && 
           !this.counterSignForm.get('checkComment1').value ||
           !this.counterSignForm.get('checkComment2').value ||
           !this.counterSignForm.get('checkComment3').value ||
           !this.counterSignForm.get('checkComment4').value;
  }

  ngOnInit(): void {
    this.initForm(this.evaluation);
    this.assignDates(this.evaluation);
  }

  ngAfterViewInit(): void {
    // after DOM has loaded assign read-only values
    this.finalEvalNum.nativeElement.value = this.evaluation.finalEvaluationQualification;
  }

  initForm(data: EvaluationModel): void {
    switch(this.evaluationRole) {
      case EvaluationRoleEnum.Evaluator:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.isLoading = false;
        this.subscribeForServiceDuringEvaluationCourseChanges();
        this.subscribeForObjectivesChanges();
        break;
      case EvaluationRoleEnum.Evaluated:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.CounterSigner:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(new EvaluationCounterSignClass(data)), this.evaluationRole);
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.EvaluatedKnow:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(new EvaluationCounterSignClass(data)), this.evaluationRole);
        this.isLoading = false;
        break;
    }
  }

  submit(action: ActionFormType): void {
    if (this.evaluationForm.invalid && action === ActionFormEnum.isSave) {
      this.evaluationForm.markAllAsTouched();
      this.showWarnning = true;
      return;
    }
    this.showWarnning = false;
    switch(action) {
      case ActionFormEnum.isSave: this.request.emit({ action, data: new EvaluationClass(this.evaluationForm.value)}); break;
      case ActionFormEnum.isConfirm: this.request.emit({ action, data: new EvaluationClass(this.evaluationForm.value)}); break;
      case ActionFormEnum.isAccept: this.request.emit({ action, data: new  EvaluationAcceptClass(this.evaluatedForm.value)}); break;
      case ActionFormEnum.isReject: this.request.emit({ action, data: new  EvaluationAcceptClass(this.evaluatedForm.value)}); break;
      case ActionFormEnum.isCounterSignAccept: this.request.emit({ action, data: new  EvaluationCounterSignClass(this.counterSignForm.value)}); break;
      case ActionFormEnum.isCounterSignReject: this.request.emit({ action, data: new  EvaluationCounterSignClass(this.counterSignForm.value)}); break;
      case ActionFormEnum.isAcknowledge: this.request.emit({ action, data: this.evaluationForm.value}); break;
    }
  }

  assignDates(data: EvaluationModel): void {
    this.parsedPeriodEvaluatedFromTo = data.periodEvaluatedFromTo; 
    this.parsedPeriodEvaluatedUpTo = data.periodEvaluatedUpTo; 
    this.parsedPeriodRunningActivityFromTo = data.periodRunningActivityFromTo; 
    this.parsedPeriodRunningActivityUpTo = data.periodEvaluatedUpTo; 
    this.parsedPartialEvaluationPeriodFromTo = data.partialEvaluationPeriodFromTo; 
    this.parsedPartialEvaluationPeriodUpTo = data.partialEvaluationPeriodUpTo; 
    this.parsedDateSanctionApplication = data.dateSanctionApplication; 
    this.parsedDateLiftingSanction = data.dateLiftingSanction; 
    this.parsedAppointmentDate = data.appointmentDate; 
    this.parsedDateEvaluationInterview = data.dateEvaluationInterview; 
    this.parsedDateSettingIindividualGoals = data.dateSettingIindividualGoals;
  }

  handleFinalQualificationChange(value: number, isInputChange: boolean = false): void {
    if (isInputChange) {
      switch(true) {
        case value >= 3.51 && value <= 4.00: this.evaluationForm?.get('finalEvaluationQualification')?.patchValue('4');break;
        case value >= 2.51 && value <= 3.50: this.evaluationForm?.get('finalEvaluationQualification')?.patchValue('3');break;
        case value >= 1.51 && value <= 2.50: this.evaluationForm?.get('finalEvaluationQualification')?.patchValue('2');break;
        case value >= 1.00 && value <= 1.50: this.evaluationForm?.get('finalEvaluationQualification')?.patchValue('1');break;
        default: this.evaluationForm.get('finalEvaluationQualification').patchValue(null);break;
      }
      return
    }

    this.finalEvalNum.nativeElement.value = +value || null;
  }

  handlePartialScoreChange(value: number, isInputChange: boolean = false): void {
    if (isInputChange) {
      switch(true) {
        case value >= 3.51 && value <= 4.00: this.evaluationForm?.get('qualifierPartialEvaluations')?.patchValue('4');break;
        case value >= 2.51 && value <= 3.50: this.evaluationForm?.get('qualifierPartialEvaluations')?.patchValue('3');break;
        case value >= 1.51 && value <= 2.50: this.evaluationForm?.get('qualifierPartialEvaluations')?.patchValue('2');break;
        case value >= 1.00 && value <= 1.50: this.evaluationForm?.get('qualifierPartialEvaluations')?.patchValue('1');break;
        default: this.evaluationForm.get('partialEvaluationScore').patchValue(null);break;
      }
      this.evaluationForm?.get('partialEvaluationScore')?.patchValue(Math.round(value));
      this.evaluationForm?.get('qualifierPartialEvaluations').markAsTouched();
      return
    }

    this.evaluationForm?.get('partialEvaluationScore')?.patchValue(value);
    this.evaluationForm?.get('partialEvaluationScore').markAsTouched();
  }

  subscribeForServiceDuringEvaluationCourseChanges(): void {
    this.evaluationForm.get('serviceDuringEvaluationCourse').valueChanges.subscribe((value: string) => {
      if (!isNaN(+value)) {
        this.evaluationForm.get('functionEvaluated').setValidators([Validators.required]);
        this.evaluationForm.get('appointmentDate').setValidators([Validators.required, Validators.pattern(isoDateRegex)]);
        this.evaluationForm.get('administrativeActService').setValidators([Validators.required]);
        this.evaluationForm.updateValueAndValidity();
      } else {
        this.evaluationForm.get('serviceDuringEvaluationCourse').patchValue(null);
        this.evaluationForm.get('functionEvaluated').patchValue(null);
        this.evaluationForm.get('appointmentDate').patchValue(null);
        this.evaluationForm.get('administrativeActService').patchValue(null);
        this.evaluationForm.get('functionEvaluated').clearValidators();
        this.evaluationForm.get('appointmentDate').clearValidators();
        this.evaluationForm.get('administrativeActService').clearValidators();
        this.evaluationForm.get('functionEvaluated').markAsUntouched();
        this.evaluationForm.get('appointmentDate').markAsUntouched();
        this.evaluationForm.get('appointmentDate').markAsPristine();
        this.evaluationForm.get('administrativeActService').markAsUntouched();
        this.evaluationForm.updateValueAndValidity();
      }
    });
  }

  subscribeForObjectivesChanges(): void {
    for(let i = 1; i < 6; i++) {
      this.evaluationForm.get('score'+i).valueChanges.subscribe((value: number)=> {
        if (!isNaN(+value)) {
          this.evaluationForm.get('goal'+i).setValidators([Validators.required]);
          this.evaluationForm.get('kpI'+i).setValidators([Validators.required]);
          this.evaluationForm.get('performanceTerm'+i).setValidators([Validators.required]);
          this.evaluationForm.get('goal'+i).patchValue(this.evaluationForm.get('goal'+i).value || null);
          this.evaluationForm.get('goal'+i).markAsTouched();
          this.evaluationForm.get('kpI'+i).patchValue(this.evaluationForm.get('kpI'+i).value || null);
          this.evaluationForm.get('kpI'+i).markAsTouched();
          this.evaluationForm.get('performanceTerm'+i).patchValue(this.evaluationForm.get('performanceTerm'+i).value || null);
          this.evaluationForm.get('performanceTerm'+i).markAsTouched();
        } else {
          this.evaluationForm.get('goal'+i).clearValidators();
          this.evaluationForm.get('kpI'+i).clearValidators();
          this.evaluationForm.get('performanceTerm'+i).clearValidators();
          this.evaluationForm.get('goal'+i).markAsUntouched();
          this.evaluationForm.get('goal'+i).updateValueAndValidity();
          this.evaluationForm.get('kpI'+i).markAsUntouched();
          this.evaluationForm.get('kpI'+i).updateValueAndValidity();
          this.evaluationForm.get('performanceTerm'+i).markAsUntouched();
          this.evaluationForm.get('performanceTerm'+i).updateValueAndValidity();
        }
      });
    }
  }

  /**
   * Calculating M1 - index 6, M2 - index 10, M3 - index 12, Pb - index 17
   * @returns {number} Media (M1, M2, M3, Pb)
   * isFixed = defaults true for UI
   */
  calculateAverageCriterias(index: number, isFixed: boolean = true): number {
    if (isFixed) {
      switch(index) {
        case 6: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc + (+this.evaluationForm?.get(`question${i+1}`)?.value || 0), 0) / 5).toFixed(2);
        case 10: return +(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+6}`)?.value || 0), 0.00) / 3).toFixed(2);
        case 12: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`score${i + 1}`)?.value || 0), 0.00) / 5).toFixed(2);
        case 17: return +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+9}`)?.value || 0), 0.00) / 4).toFixed(2);
      }
    }

    switch(index) {
      case 6: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc + (+this.evaluationForm?.get(`question${i+1}`)?.value || 0), 0) / 5)
      case 10: return +(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+6}`)?.value || 0), 0.00) / 3)
      case 12: return +(Array.from(Array(5).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`score${i + 1}`)?.value || 0), 0.00) / 5)
      case 17: return +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+9}`)?.value || 0), 0.00) / 4)
    }
  }

  /**
   * 
   * @returns {number} Pregatirea de baza (M4)
   * 
   * pb - pregatirea de baza (1-4)
   * pf - pregatirea fizica
   */
  calculateM4(isFixed: boolean = true): number {
    const pb = +(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+9}`)?.value || 0), 0.00) / 4);
    const pf = +this.evaluationForm?.get('question13')?.value || 0;

    return isFixed ? +((pb + pf) / 2).toFixed(2) : (pb + pf) / 2;
  }

  /**
   * Punctajul la evaluarea anuala (MEa)
   */
  calculateMea(isFixed: boolean = true): number {
    const m1 = +this.calculateAverageCriterias(6, false)
    const m2 = +this.calculateAverageCriterias(10, false)
    const m3 = this.calculateAverageCriterias(12, false);
    const m4 = this.calculateM4(false);

    return isFixed ? +((m1 + m2 + m3 + m4) / 4).toFixed(2) : (m1 + m2 + m3 + m4) / 4; 
  }

  /**
   * 
   * @returns {number} Evaluare anuala final (Mf)
   */
  calculateMf(isFixed: boolean = true): number | string {
    const Mea = this.calculateMea(false);
    const Mep = +this.evaluationForm?.get('partialEvaluationScore')?.value || 0;
    
    if (this.evaluationForm?.get('partialEvaluationScore')?.value) {
      return isFixed ? ((Mea + Mep) / 2).toFixed(2) : (Mea + Mep) / 2; 
    }

    return isFixed ? (Mea).toFixed(2) : Mea;
  }

  precompleteWith(btn, value: number): void {
    for(let i = 1; i < 14; i++) {
      this.evaluationForm.get('question'+i).patchValue(+value);
      this.evaluationForm.get('question'+i).markAsTouched();

      if (i > 0 && i < 6) {
        this.evaluationForm.get('score'+i).patchValue(+value);
        this.evaluationForm.get('score'+i).markAsTouched();
      }
    }
    btn.click();
  }

async randomCompleteForAvg(btn, value) {
  new Promise((resolve, reject) => {
    this.isLoading = true;
    btn.click();
    return setTimeout(() => resolve(true), 300);
  }).then(() => {
    return generateAvgNumbers(+value);
    }).then((result: any) => {
      for(let i = 1; i < 14; i++) {
        this.evaluationForm.get('question'+i).patchValue(result.questions[i - 1]);
        this.evaluationForm.get('question'+i).markAsTouched();
  
        if (i > 0 && i < 6) {
          this.evaluationForm.get('score'+i).patchValue(result.scores[i - 1]);
          this.evaluationForm.get('score'+i).markAsTouched();
        }
      }
      this.isLoading = false;
    })
  }
}
