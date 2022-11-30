import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
export class CreateComponent implements OnInit {
  evaluationForm: FormGroup;
  questions: number[] = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15];
  radios: number[] = [1,2,3,4];
  criterias = [
    'Capacitatea de a planifica, organiza, coordona, monitoriza si evalua activitatea subdiviziunii conduse',
    'Capacitatea de a gestiona eficient activitatea personalului prin repartizarea in mod echilibrat a sarcinilor de serviciu',
    'Capacitatea de a lua decizii in mod operativ, de a-si asuma riscurile si responsabilitatea pentru deciziile',
    'Mentinerea unui climat optim de munca a subordonatilor',
    'Capacitatea de a-si indeplini atributiile cu exactitate complet si calitativ',
    'Capacitatea de a gestiona, utiliza, intocmi si aplica documentele de serviciu',
    'Nivel de realizare a sarcinilor de serviciu si obiectivelor individuale',
    'Executarea ordinelor si dispozitiilor conducatorilor, operativitatea in realizarea misinilor',
    'Respectarea eticii profesionale (comportamentul cu sefii, subordonatii, cetatenii)',
    'Abateri disciplinare (se va puncta: avertisment/observatie: -:; alte sanctiuni - 1; lipsa - 3 sau 4, dupa caz)',
    'Cunostinte la pregatirea generala',
    'Cunostinte la pregatirea de specialitate',
    'Instructia tragerii (TS - dupa caz)',
    'Interventia profesionala (normative de lupta, SPIGF)',
    'Pregatirea fizica: (PF)'
  ];
  parsedDateCompletionGeneralData: string = '';
  parsedPeriodEvaluatedFromTo: string = '';
  parsedPeriodEvaluatedUpTo: string = '';
  
  parsedLastQualifyCategoryOrderDate: string = '';
  parsedPartialEvaluationFromDate: string = '';
  parsedPartialEvaluationToDate: string = '';
  parsedSanctionStartDate: string = '';
  parsedSanctionEndDate: string = '';
  parsedInterviewDate: string = '';
  parsedAcceptanceDate: string = '';
  parsedEvaluatingDate: string = '';
  parsedCounterSignerSignDate: string = '';
  constructor(private readonly fb: FormBuilder,
              private readonly evaluationService: EvaluationService,
              private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.initForm({
      id: 2,
      evaluatedName: "RERU Client Evaluation",
      subdivisionName: null,
      dateCompletionGeneralData: "0001-01-01T00:00:00",
      nameSurnameEvaluatedEmployee: null,
      functionSubdivision: null,
      specialOrMilitaryGrade: null,
      periodEvaluatedFromTo: null,
      periodEvaluatedUpTo: null,
      education: null,
      professionalTrainingActivities: null,
      courseName: null,
      periodRunningActivity: null,
      administrativeActOfStudies: null,
      modificationServiceReportDuringEvaluationCourse: null,
      function: null,
      appointmentDate: null,
      administrativeActService: null,
      partialEvaluationPeriod: null,
      finalScorePartialEvaluations: null,
      qualifierPartialEvaluations: null,
      sanctionAppliedEvaluationCourse: null,
      dateSanctionApplication: null,
      dateLiftingSanction: null,
      qualificationEvaluationObtained2YearsPast: null,
      qualificationEvaluationObtainedPreviousYear: null,
      qualificationQuarter1: null,
      qualificationQuarter2: null,
      qualificationQuarter3: null,
      qualificationQuarter4: null,
      question1: null,
      question2: null,
      question3: null,
      question4: null,
      question5: null,
      question6: null,
      question7: null,
      question8: null,
      question9: null,
      question10: null,
      question11: null,
      question12: null,
      question13: null,
      goal1: null,
      goal2: null,
      goal3: null,
      goal4: null,
      goal5: null,
      kpI1: null,
      kpI2: null,
      kpI3: null,
      kpI4: null,
      kpI5: null,
      performanceTerm1: null,
      performanceTerm2: null,
      performanceTerm3: null,
      performanceTerm4: null,
      performanceTerm5: null,
      score1: null,
      score2: null,
      score3: null,
      score4: null,
      score5: null,
      finalEvaluationQualification: null,
      dateEvaluatiorInterview: null,
      dateSettingIindividualGoals: null,
      need1ProfessionalDevelopmentEvaluatedEmployee: null,
      need2ProfessionalDevelopmentEvaluatedEmployee: null,
      evaluatorComments: null
    })
  }

  initForm(data?: Evaluation): void {
    this.evaluationForm = this.fb.group({
      id: this.fb.control(data?.id, []),
      evaluatedName: this.fb.control(data?.evaluatedName, []),
      subdivisionName: this.fb.control(data?.subdivisionName, []),
      dateCompletionGeneralData: this.fb.control(data?.dateCompletionGeneralData, []),
      nameSurnameEvaluatedEmployee: this.fb.control(data?.nameSurnameEvaluatedEmployee, []),
      functionSubdivision: this.fb.control(data?.functionSubdivision, []),
      specialOrMilitaryGrade: this.fb.control(data?.specialOrMilitaryGrade, []),
      periodEvaluatedFromTo: this.fb.control(data?.periodEvaluatedFromTo, []),
      periodEvaluatedUpTo: this.fb.control(data?.periodEvaluatedUpTo, []),
      education: this.fb.control(data?.education, []),
      professionalTrainingActivities: this.fb.control(data?.professionalTrainingActivities, []),
      courseName: this.fb.control(data?.courseName, []),
      periodRunningActivity: this.fb.control(data?.periodRunningActivity, []),
      administrativeActOfStudies: this.fb.control(data?.administrativeActOfStudies, []),
      modificationServiceReportDuringEvaluationCourse: this.fb.control(data?.modificationServiceReportDuringEvaluationCourse, []),
      function: this.fb.control(data?.function, []),
      appointmentDate: this.fb.control(data?.appointmentDate, []),
      administrativeActService: this.fb.control(data?.administrativeActService, []),
      partialEvaluationPeriod: this.fb.control(data?.partialEvaluationPeriod, []),
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
      need1ProfessionalDevelopmentEvaluatedEmployee: this.fb.control(data?.need1ProfessionalDevelopmentEvaluatedEmployee, []),
      need2ProfessionalDevelopmentEvaluatedEmployee: this.fb.control(data?.need2ProfessionalDevelopmentEvaluatedEmployee, []),
      evaluatorComments: this.fb.control(data?.evaluatorComments, []),
    });
  }

  parseDate(value: Moment, formControlName: string): void {
    const date = moment(value).toDate();
    this.evaluationForm.get(formControlName).patchValue(new Date(date).toISOString());
  }

  submit(): void {
    this.evaluationService.create(this.preParseData(this.evaluationForm.value)).subscribe(response => {
      this.notificationService.success('Success', 'Fisa a fost trimisă cu success!', NotificationUtil.getDefaultMidConfig());
      this.router.navigate(['../'], { relativeTo: this.route});
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

}
