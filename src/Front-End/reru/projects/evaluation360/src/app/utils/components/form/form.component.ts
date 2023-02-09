import { AfterViewInit, Component, ElementRef, EventEmitter, Input, NgZone, OnInit, Output, ViewChild } from '@angular/core';
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
         isInvalidCustom,
         isoDateRegex, 
} from '../../util/forms.util';
import { parseEvaluatedModel, parseCounterSignModel, parseDate, generateAvgNumbers } from '../../util/parsings.util';
import { EvaluationRoleEnum } from '../../models/evaluation-role.enum';
import { EvaluationTypeEnum } from '../../models/type.enum';
import { SanctionEnum } from '../../models/sanction.enum';
import { ActionFormEnum, ActionFormModel, ActionFormType } from '../../models/action-form.type';
import { EvaluationAcceptClass } from '../../models/evaluation-accept.model';
import { EvaluationCounterSignClass } from '../../models/evaluation-countersign.model';
import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { startWith } from 'rxjs/operators';

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
  sanctionEnum = SanctionEnum;

  @ViewChild('finalEvalNum') finalEvalNum: ElementRef;
  @ViewChild('commentsEvaluated') commentsEvaluated: ElementRef;
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
  isEvaluatorRoleView: boolean;
  isEvaluatedRoleView: boolean;
  isCounterSignerView: boolean;
  isEvaluatedKnowView: boolean;
  isDisabledEvaluator: boolean;
  isDisabledEvaluated: boolean;
  isDisabledCounterSigner: boolean;
  isNotCheckedCounterSigner: boolean;

  M1: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  M1$: Observable<number> = this.M1.asObservable();
  M2: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  M2$: Observable<number> = this.M2.asObservable();
  M3: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  M3$: Observable<number> = this.M3.asObservable();
  Pb: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  Pb$: Observable<number> = this.Pb.asObservable();
  M4: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  M4$: Observable<number> = this.M4.asObservable();
  Mea: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  Mea$: Observable<number> = this.Mea.asObservable();
  Mf: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  Mf$: Observable<number> = this.Mf.asObservable();

  showSanctionSection$: Subject<boolean> = new Subject();

  constructor(private readonly ngZone: NgZone) {
    this.isInvalidPattern = isInvalidPattern.bind(this);
    this.isValid = isValid.bind(this);
    this.isInvalidRequired = isInvalidRequired.bind(this);
    this.parseDate = parseDate.bind(this);
    this.isInvalidMin = isInvalidMin.bind(this);
    this.isInvalidMax = isInvalidMax.bind(this);
    this.isInvalidCustom = isInvalidCustom.bind(this);
   }

  ngOnInit(): void {
    this.initForm(this.evaluation);
    this.assignDates(this.evaluation);

    this.isEvaluatorRoleView = [EvaluationRoleEnum.Evaluator,
      EvaluationRoleEnum.Evaluated,
      EvaluationRoleEnum.CounterSigner,
      EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole);

    this.isEvaluatedRoleView = [EvaluationRoleEnum.Evaluated, 
      EvaluationRoleEnum.CounterSigner, 
      EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)

    this.isCounterSignerView = !this.evaluation.counterSignerName ? false : [EvaluationRoleEnum.CounterSigner,
        EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole)

    this.isEvaluatedKnowView = [EvaluationRoleEnum.EvaluatedKnow].includes(this.evaluationRole);

    this.isDisabledEvaluator = this.evaluationRole != EvaluationRoleEnum.Evaluator;

    this.isDisabledEvaluated = this.evaluationRole != EvaluationRoleEnum.Evaluated;

    this.isDisabledCounterSigner = this.evaluationRole != EvaluationRoleEnum.CounterSigner;
  }

  subscribeForCounterSignerChanges(): void {
    combineLatest([
      this.counterSignForm.get('checkComment1').valueChanges.pipe(startWith('')),
      this.counterSignForm.get('checkComment2').valueChanges.pipe(startWith('')),
      this.counterSignForm.get('checkComment3').valueChanges.pipe(startWith('')),
      this.counterSignForm.get('checkComment4').valueChanges.pipe(startWith(''))
    ]).subscribe(() => {
      this.isNotCheckedCounterSigner = !this.counterSignForm.get('checkComment1').value ||
                                      !this.counterSignForm.get('checkComment2').value ||
                                      !this.counterSignForm.get('checkComment3').value ||
                                      !this.counterSignForm.get('checkComment4').value;
    });
  }

  ngAfterViewInit(): void {
    // after DOM has loaded assign read-only values
    this.Mf$.subscribe((value) => this.finalEvalNum.nativeElement.value = !isNaN(value) ? value.toFixed(2) : 0) ;
    this.focusEvaluatedCommentsArea();
    this.subscribeForSanctionChanges();
  }

  subscribeForSanctionChanges(): void {
    this.evaluationForm.get('sanctionApplied').valueChanges.subscribe((value: number) => {
      if (isNaN(+value) || +value === this.sanctionEnum.Without) {
        this.showSanctionSection$.next(false);
        return;
      }

      this.showSanctionSection$.next(true);
    })
  }

  focusEvaluatedCommentsArea(): void {
    if (this.evaluationRole === EvaluationRoleEnum.Evaluated) {
      this.commentsEvaluated.nativeElement.scrollIntoView({ behavior: 'smooth' });
      this.ngZone.runOutsideAngular(() => {
        setTimeout(() => {
          this.commentsEvaluated.nativeElement.focus();
        }, 500);
      })
    }
  }

  initForm(data: EvaluationModel): void {
    switch(this.evaluationRole) {
      case EvaluationRoleEnum.Evaluator:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.isLoading = false;
        this.subscribeForServiceDuringEvaluationCourseChanges();
        this.subscribeForObjectivesChanges();
        this.subscribeForQualificationsChanges();
        break;
      case EvaluationRoleEnum.Evaluated:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.subscribeForQualificationsChanges();
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.CounterSigner:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(new EvaluationCounterSignClass(data)), this.evaluationRole);
        this.subscribeForQualificationsChanges();
        this.subscribeForCounterSignerChanges();
        this.isLoading = false;
        break;
      case EvaluationRoleEnum.EvaluatedKnow:
        this.evaluationForm = createEvaluatorForm(data, this.evaluationRole);
        this.evaluatedForm = createEvaluatedForm(parseEvaluatedModel(new EvaluationAcceptClass(data)), this.evaluationRole);
        this.counterSignForm = createCounterSignForm(parseCounterSignModel(new EvaluationCounterSignClass(data)), this.evaluationRole);
        this.subscribeForQualificationsChanges();
        this.isLoading = false;
        break;
    }
  }

  private subscribeForQualificationsChanges(): void {
    this.subscribeForM1Changes();
    this.subscribeForM2Changes();
    this.subscribeForM3Changes();
    this.subscribeForPbChanges();
    this.subscribeForM4Changes();
    this.subscribeForMeaChanges();
    this.subscribeForMfChanges();
  }

  subscribeForM1Changes(): void {
    combineLatest([
        this.evaluationForm.get('question1').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question2').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question3').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question4').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question5').valueChanges.pipe(startWith('')),
      ]).subscribe(() => {
        this.M1.next(+(Array.from(Array(5).keys()).reduce((acc, _, i) => acc + (+this.evaluationForm?.get(`question${i+1}`)?.value || 0), 0) / 5));
      });
  }

  subscribeForM2Changes(): void {
    combineLatest([
        this.evaluationForm.get('question6').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question7').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question8').valueChanges.pipe(startWith(''))
      ]).subscribe(() => {
        this.M2.next(+(Array.from(Array(3).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+6}`)?.value || 0), 0.00) / 3));
      });
  }

  subscribeForM3Changes(): void {
    combineLatest([
        this.evaluationForm.get('score1').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('score2').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('score3').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('score4').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('score5').valueChanges.pipe(startWith(''))
      ]).subscribe(() => {
        this.M3.next(+(Array.from(Array(5).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`score${i + 1}`)?.value || 0), 0.00) / 5));
      });
  }

  subscribeForPbChanges(): void {
    combineLatest([
        this.evaluationForm.get('question9').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question10').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question11').valueChanges.pipe(startWith('')),
        this.evaluationForm.get('question12').valueChanges.pipe(startWith(''))
      ]).subscribe(() => {
        this.Pb.next(+(Array.from(Array(4).keys()).reduce((acc, _, i) => acc +(+this.evaluationForm?.get(`question${i+9}`)?.value || 0), 0.00) / 4));
      });
  }

  subscribeForM4Changes(): void {
    combineLatest([
        this.Pb$,
        this.evaluationForm.get('question13').valueChanges.pipe(startWith(''))
      ]).subscribe(([pb]) => {
        const pf = +this.evaluationForm?.get('question13')?.value || 0;
        this.M4.next((pb + pf) / 2);
      });
  }

  subscribeForMeaChanges(): void {
    combineLatest([
      this.M1$,
      this.M2$,
      this.M3$,
      this.M4$,
    ]).subscribe(([m1, m2, m3, m4]) => {
      this.Mea.next((m1 + m2 + m3 + m4) / 4);
    });
  }

  subscribeForMfChanges(): void {
    combineLatest([
      this.Mea$,
      this.evaluationForm?.get('partialEvaluationScore')?.valueChanges.pipe(startWith(''))
    ]).subscribe(([mea]) => {
      if (this.evaluationForm?.get('partialEvaluationScore')?.value) {
        const Mep = this.evaluationForm?.get('partialEvaluationScore')?.value || 0;
        this.handleFinalQualificationChange(Math.round((mea + Mep) / 2), true);
        this.evaluationForm.get('finalEvaluationQualification').markAsTouched();
        this.Mf.next((mea + Mep) / 2);
        return;
      }

      this.handleFinalQualificationChange(Math.round(mea), true);
      this.evaluationForm.get('finalEvaluationQualification').markAsTouched();
      this.Mf.next(mea);
    });
  }

  submit(action: ActionFormType): void {
    if (this.evaluationForm.invalid && action === ActionFormEnum.isSave) {
      this.evaluationForm.markAllAsTouched();
      this.showWarnning = true;
      return;
    }
    this.showWarnning = false;
    switch(action) {
      case ActionFormEnum.isSave: this.request.emit({ action, data: new EvaluationClass(this.evaluationForm.getRawValue())}); break;
      case ActionFormEnum.isConfirm: this.request.emit({ action, data: new EvaluationClass(this.evaluationForm.getRawValue())}); break;
      case ActionFormEnum.isAccept: this.request.emit({ action, data: new  EvaluationAcceptClass(this.evaluatedForm.getRawValue())}); break;
      case ActionFormEnum.isReject: this.request.emit({ action, data: new  EvaluationAcceptClass(this.evaluatedForm.getRawValue())}); break;
      case ActionFormEnum.isCounterSignAccept: this.request.emit({ action, data: new  EvaluationCounterSignClass(this.counterSignForm.getRawValue())}); break;
      case ActionFormEnum.isCounterSignReject: this.request.emit({ action, data: new  EvaluationCounterSignClass(this.counterSignForm.getRawValue())}); break;
      case ActionFormEnum.isAcknowledge: this.request.emit({ action, data: this.evaluationForm.getRawValue()}); break;
    }
  }

  assignDates(data: EvaluationModel): void {
    this.parsedPeriodEvaluatedFromTo = data.periodEvaluatedFromTo; 
    this.parsedPeriodEvaluatedUpTo = data.periodEvaluatedUpTo; 
    this.parsedPeriodRunningActivityFromTo = data.periodRunningActivityFromTo; 
    this.parsedPeriodRunningActivityUpTo = data.periodRunningActivityUpTo; 
    this.parsedPartialEvaluationPeriodFromTo = data.partialEvaluationPeriodFromTo; 
    this.parsedPartialEvaluationPeriodUpTo = data.partialEvaluationPeriodUpTo; 
    this.parsedDateSanctionApplication = data.dateSanctionApplication; 
    this.parsedDateLiftingSanction = data.dateLiftingSanction; 
    this.parsedAppointmentDate = data.appointmentDate; 
    this.parsedDateEvaluationInterview = data.dateEvaluationInterview; 
    this.parsedDateSettingIindividualGoals = data.dateSettingIindividualGoals;
  }

  handleFinalQualificationChange(value: number | string, isInputChange: boolean = false): void {
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

    if (this.finalEvalNum) {
      this.finalEvalNum.nativeElement.value = +value % 2 === 0 ? value : +value || null;
    }
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
        
        this.evaluationForm.get('functionEvaluated').updateValueAndValidity();
        this.evaluationForm.get('appointmentDate').updateValueAndValidity();
        this.evaluationForm.get('administrativeActService').updateValueAndValidity();
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

        this.evaluationForm.get('functionEvaluated').updateValueAndValidity();
        this.evaluationForm.get('appointmentDate').updateValueAndValidity();
        this.evaluationForm.get('administrativeActService').updateValueAndValidity();
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
  new Promise((resolve, _) => {
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
