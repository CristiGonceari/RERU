import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SurveyService } from '../../../utils/services/survey.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DatePipe } from '@angular/common';
import { Moment } from 'moment';
import * as moment from 'moment';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  surveyForm: FormGroup;
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
  parsedStartDate: string = '';
  parsedEvaluationFromDate: string = '';
  parsedEvaluationToDate: string = '';
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
              private readonly surveyService: SurveyService,
              private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notificationService: NotificationsService,
              private readonly datePipe: DatePipe) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.surveyForm = this.fb.group({
      department: this.fb.control(null, []),
      startDate: this.fb.control(null, []),
      evaluatedName: this.fb.control(null, []),
      evaluatedLastName: this.fb.control(null, []),
      evaluatedFatherName: this.fb.control(null, []),
      evaluatedDepartmentName: this.fb.control(null, []),
      evaluatedPosition: this.fb.control(null, []),
      specialRank: this.fb.control(null, []),
      evaluationFromDate: this.fb.control(null, []),
      evaluationToDate: this.fb.control(null, []),
      specializationCoursesInternal: this.fb.control(null, []),
      specializationCoursesInternational: this.fb.control(null, []),
      specializationCoursesOthers: this.fb.control(null, []),
      degree: this.fb.control(null, []),
      lastQualifyCategory: this.fb.control(null, []),
      lastQualifyCategoryOrderSeries: this.fb.control(null, []),
      lastQualifyCategoryOrderNumber: this.fb.control(null, []),
      lastQualifyCategoryOrderDate: this.fb.control(null, []),
      treeYearsAgoMark: this.fb.control(null, []),
      twoYearsAgoMark: this.fb.control(null, []),
      lastYearMark: this.fb.control(null, []),
      partialEvaluationFromDate: this.fb.control(null, []),
      partialEvaluationToDate: this.fb.control(null, []),
      partialEvaluationRate: this.fb.control(null, []),
      partialEvaluationMark: this.fb.control(null, []),
      sanctionStartDate: this.fb.control(null, []),
      sanctionEndDate: this.fb.control(null, []),
      question1SelfEvaluationMark: this.fb.control(null, []),
      question1Mark: this.fb.control(null, []),
      question2SelfEvaluationMark: this.fb.control(null, []),
      question2Mark: this.fb.control(null, []),
      question3SelfEvaluationMark: this.fb.control(null, []),
      question3Mark: this.fb.control(null, []),
      question4SelfEvaluationMark: this.fb.control(null, []),
      question4Mark: this.fb.control(null, []),
      question5SelfEvaluationMark: this.fb.control(null, []),
      question5Mark: this.fb.control(null, []),
      question6SelfEvaluationMark: this.fb.control(null, []),
      question6Mark: this.fb.control(null, []),
      question7SelfEvaluationMark: this.fb.control(null, []),
      question7Mark: this.fb.control(null, []),
      question8SelfEvaluationMark: this.fb.control(null, []),
      question8Mark: this.fb.control(null, []),
      question9SelfEvaluationMark: this.fb.control(null, []),
      question9Mark: this.fb.control(null, []),
      question10SelfEvaluationMark: this.fb.control(null, []),
      question10Mark: this.fb.control(null, []),
      question11SelfEvaluationMark: this.fb.control(null, []),
      question11Mark: this.fb.control(null, []),
      question12SelfEvaluationMark: this.fb.control(null, []),
      question12Mark: this.fb.control(null, []),
      question13SelfEvaluationMark: this.fb.control(null, []),
      question13Mark: this.fb.control(null, []),
      question14SelfEvaluationMark: this.fb.control(null, []),
      question14Mark: this.fb.control(null, []),
      question15SelfEvaluationMark: this.fb.control(null, []),
      question15Mark: this.fb.control(null, []),
      
      interviewDate: this.fb.control(null, []),
      comments: this.fb.control(null, []),
      individualObjective1: this.fb.control(null, []),
      individualObjective2: this.fb.control(null, []),
      individualObjective3: this.fb.control(null, []),
      needImprovement1: this.fb.control(null, []),
      needImprovement2: this.fb.control(null, []),
      needImprovement3: this.fb.control(null, []),
      evaluatedComments: this.fb.control(null, []),
      evaluatedAcceptance: this.fb.control(null, []),
      acceptanceDate: this.fb.control(null, []),
      evaluatedSigned: this.fb.control(null, []),
      evaluatingFullName: this.fb.control(null, []),
      evaluatingPosition: this.fb.control(null, []),
      evaluatingDate: this.fb.control(null, []),
      evaluatingSigned: this.fb.control(null, []),
      counterSingerComments: this.fb.control(null, []),
      counterSignerAcceptance: this.fb.control(null, []),
      counterSignerName: this.fb.control(null, []),
      counterSignerPosition: this.fb.control(null, []),
      counterSignerSignDate: this.fb.control(null, []),
      counterSignerSigned: this.fb.control(null, [])
    });
  }

  parseDate(value: Moment, formControlName: string): void {
    const date = moment(value).toDate();
    this.surveyForm.get(formControlName).patchValue(new Date(date).toISOString());
  }

  submit(): void {
    this.surveyService.create(this.preParseData(this.surveyForm.value)).subscribe(response => {
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
