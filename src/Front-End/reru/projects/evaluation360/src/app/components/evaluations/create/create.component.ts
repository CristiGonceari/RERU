import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EvaluationService } from '@utils/services';
import { ObjectUtil, NotificationUtil, Evaluation } from '@utils';
import { Moment } from 'moment';
import * as moment from 'moment';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit, OnDestroy {
  isLoading: boolean = true;
  evaluationForm: FormGroup;
  questions: number[] = [1,2,3,4,5,6,7,8,9,10,11,12,13];
  criterias: string[] = [
    'Capacitatea de a realiza deplin sarcinile de serviciu',
    'Capacitatea de a depăși obstacolele sau dificultățile intervenite',
    'Capacitatea de asumare a responsabilităților, de asumare a erorilor în activitate',
    'Capacitatea de analiză și sinteză, creativitate și spirit de inițiativă',
    'Capacitatea de a lucra în echipă, de a-și aduce contribuția prin participare efectivă',
    'Capacitatea de a promova și respecta normele de conduită la serviciu și în afara acestuia, în relații cu conducătorii, colegii și alte persoane',
    'Capacitatea de a executa corect prevederile actelor normative și a disciplinii de serviciu',
    'Sancțiuni disciplinare (Se va puncta: avertisment/observaţie – 2 puncte; alte sancţiuni – 1 puncte; lipsa – 3 sau 4, după caz)',
    'Pregătirea generală',
    'Pregătirea de specialitate',
    'Instrucţia tragerii (TS – după caz)',
    'Intervenţia profesională, (Normative de luptă, SPÎGF)',
    'Pregătirea fizică: (P f )'
  ];
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

  constructor(private readonly fb: FormBuilder,
              private readonly evaluationService: EvaluationService,
              private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe((params: Params) => {
      if (params.id) {
        this.evaluationService.get(params.id).subscribe((response: any) => {
          this.initForm(response.data);
          this.assignDates(response.data);
          this.isLoading = false;
        }, error => {
          this.isLoading = false;
          this.router.navigate(['../../evaluation'])
        });
      } else {
        this.router.navigate(['../../evaluation'])
      }
    })
  }

  initForm(data?: Evaluation): void {
    this.evaluationForm = this.fb.group({
      id: this.fb.control(data?.id, []),
      subdivisionName: this.fb.control(data?.subdivisionName, []),
      evaluatedName: this.fb.control({value: data?.evaluatedName, disabled: true }, []),
      dateCompletionGeneralData: this.fb.control(data?.dateCompletionGeneralData, []),
      nameSurnameEvaluated: this.fb.control(data?.nameSurnameEvaluated, []),
      functionSubdivision: this.fb.control(data?.functionSubdivision, []),
      specialOrMilitaryGrade: this.fb.control(data?.specialOrMilitaryGrade, []),
      specialOrMilitaryGradeText: this.fb.control(data?.specialOrMilitaryGradeText, []),
      periodEvaluatedFromTo: this.fb.control(data?.periodEvaluatedFromTo, []),
      periodEvaluatedUpTo: this.fb.control(data?.periodEvaluatedUpTo, []),
      educationEnum: this.fb.control(data?.educationEnum, []),
      professionalTrainingActivities: this.fb.control(data?.professionalTrainingActivities, []),
      professionalTrainingActivitiesType: this.fb.control(data?.professionalTrainingActivitiesType, []),
      courseName: this.fb.control(data?.courseName, []),
      periodRunningActivityFromTo: this.fb.control(data?.periodRunningActivityFromTo, []),
      periodRunningActivityUpTo: this.fb.control(data?.periodRunningActivityUpTo, []),
      administrativeActOfStudies: this.fb.control(data?.administrativeActOfStudies, []),
      serviceDuringEvaluationCourse: this.fb.control(data?.serviceDuringEvaluationCourse),
      functionEvaluated: this.fb.control(data?.functionEvaluated, []),
      appointmentDate: this.fb.control(data?.appointmentDate, []),
      administrativeActService: this.fb.control(data?.administrativeActService, []),
      partialEvaluationPeriodFromTo: this.fb.control(data?.partialEvaluationPeriodFromTo, []),
      partialEvaluationPeriodUpTo: this.fb.control(data?.partialEvaluationPeriodUpTo, []),
      finalScorePartialEvaluations: this.fb.control(data?.finalScorePartialEvaluations, []),
      qualifierPartialEvaluations: this.fb.control(data?.qualifierPartialEvaluations, []),
      sanctionAppliedEvaluationCourse: this.fb.control(data?.sanctionAppliedEvaluationCourse, []),
      dateSanctionApplication: this.fb.control(data?.dateSanctionApplication, []),
      dateLiftingSanction: this.fb.control(data?.dateLiftingSanction, []),
      qualificationEvaluationObtained2YearsPast: this.fb.control(data?.qualificationEvaluationObtained2YearsPast, []),
      qualificationEvaluationObtainedPreviousYear: this.fb.control(data?.qualificationEvaluationObtainedPreviousYear, []),
      qualificationQuarter1: this.fb.control(data?.qualificationQuarter1, []),
      qualificationQuarter2: this.fb.control(data?.qualificationQuarter2, []),
      qualificationQuarter3: this.fb.control(data?.qualificationQuarter3, []),
      qualificationQuarter4: this.fb.control(data?.qualificationQuarter4, []),
      question1: this.fb.control(data?.question1, []),
      question2: this.fb.control(data?.question2, []),
      question3: this.fb.control(data?.question3, []),
      question4: this.fb.control(data?.question4, []),
      question5: this.fb.control(data?.question5, []),
      question6: this.fb.control(data?.question6, []),
      question7: this.fb.control(data?.question7, []),
      question8: this.fb.control(data?.question8, []),
      question9: this.fb.control(data?.question9, []),
      question10: this.fb.control(data?.question10, []),
      question11: this.fb.control(data?.question11, []),
      question12: this.fb.control(data?.question12, []),
      question13: this.fb.control(data?.question13, []),
      goal1: this.fb.control(data?.goal1, []),
      goal2: this.fb.control(data?.goal2, []),
      goal3: this.fb.control(data?.goal3, []),
      goal4: this.fb.control(data?.goal4, []),
      goal5: this.fb.control(data?.goal5, []),
      kpI1: this.fb.control(data?.kpI1, []),
      kpI2: this.fb.control(data?.kpI2, []),
      kpI3: this.fb.control(data?.kpI3, []),
      kpI4: this.fb.control(data?.kpI4, []),
      kpI5: this.fb.control(data?.kpI5, []),
      performanceTerm1: this.fb.control(data?.performanceTerm1, []),
      performanceTerm2: this.fb.control(data?.performanceTerm2, []),
      performanceTerm3: this.fb.control(data?.performanceTerm3, []),
      performanceTerm4: this.fb.control(data?.performanceTerm4, []),
      performanceTerm5: this.fb.control(data?.performanceTerm5, []),
      score1: this.fb.control(data?.score1, []),
      score2: this.fb.control(data?.score2, []),
      score3: this.fb.control(data?.score3, []),
      score4: this.fb.control(data?.score4, []),
      score5: this.fb.control(data?.score5, []),
      finalEvaluationQualification: this.fb.control(data?.finalEvaluationQualification, []),
      dateEvaluatiorInterview: this.fb.control(data?.dateEvaluatiorInterview, []),
      dateSettingIindividualGoals: this.fb.control(data?.dateSettingIindividualGoals, []),
      need1ProfessionalDevelopmentEvaluated: this.fb.control(data?.need1ProfessionalDevelopmentEvaluated, []),
      need2ProfessionalDevelopmentEvaluated: this.fb.control(data?.need2ProfessionalDevelopmentEvaluated, []),
      commentsEvaluator: this.fb.control(data?.commentsEvaluator, []),
    });
  }

  assignDates(data: Evaluation): void {
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

  parseDate(value: Moment, formControlName: string): void {
    const date = moment(value).toDate();
    this.evaluationForm.get(formControlName).patchValue(new Date(date).toISOString());
  }

  submit(isConfirm: boolean = false): void {
    this.evaluationService[isConfirm ? 'confirm' : 'update'](this.preParseData(this.evaluationForm.value)).subscribe(response => {
      this.notificationService.success('Success', 
                                        isConfirm? 'Fisa a fost trimisă cu success!' : 'Fisa a fost salvata cu succes!', 
                                       NotificationUtil.getDefaultMidConfig());
      if (isConfirm) {
        this.router.navigate(['../../'], { relativeTo: this.route});
      }
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  preParseData(data) {
    const old = {}
    for (const property in data) {
      if (data[property] && property.toLowerCase().includes('signed')) {
        old[property] = !!data[property];
      } else if (!isNaN(data[property])) {
        old[property] = +data[property];
      } else {
        old[property] = data[property];
      }
    }

    return ObjectUtil.preParseObject(old);
  }

  ngOnDestroy(): void {
    console.log('destroyed')
  }

}
