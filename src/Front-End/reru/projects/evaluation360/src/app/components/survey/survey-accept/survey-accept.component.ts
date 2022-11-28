import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { SurveyService } from '../../../utils/services/survey.service';

@Component({
  selector: 'app-survey-accept',
  templateUrl: './survey-accept.component.html',
  styleUrls: ['./survey-accept.component.scss']
})
export class SurveyAcceptComponent implements OnInit {
  surveyForm: FormGroup;
  questions: number[] = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15];
  radios: number[] = [1,2,3,4];
  id: number;
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
  isLoading: boolean = true;
  constructor(private fb: FormBuilder,
              private surveyService: SurveyService,
              private route: ActivatedRoute,
              private notificationService: NotificationsService,
              private router: Router,
              private ngZone: NgZone) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.id = +response.id;
        this.retrieveEvaluation(+response.id);
      }
    })
  }

  retrieveEvaluation(id: number) {
    this.surveyService.get(id).subscribe(response => {
      this.initForm(response);
      this.isLoading = false;
    });
  }

  initForm(data: any = {}): void {
    this.surveyForm = this.fb.group({
      department: this.fb.control({value: data.department, disabled: true }, []),
      startDate: this.fb.control({ value: data.startDate, disabled: true }, []),
      evaluatedName: this.fb.control({ value: data.evaluatedName, disabled: true}, []),
      evaluatedLastName: this.fb.control({ value: data.evaluatedLastName, disabled: true }, []),
      evaluatedFatherName: this.fb.control({ value: data.evaluatedFatherName, disabled: true }, []),
      evaluatedDepartmentName: this.fb.control({ value: data.evaluatedDepartmentName, disabled: true }, []),
      evaluatedPosition: this.fb.control({ value: data.evaluatedPosition, disabled: true} , []),
      specialRank: this.fb.control({ value: data.specialRank, disabled: true}, []),
      evaluationFromDate: this.fb.control({ value: data.evaluationFromDate, disabled: true}, []),
      evaluationToDate: this.fb.control({ value: data.evaluationToDate, disabled: true}, []),
      specializationCoursesInternal: this.fb.control({ value: data.specializationCoursesInternal, disabled: true}, []),
      specializationCoursesInternational: this.fb.control({ value: data.specializationCoursesInternational, disabled: true}, []),
      specializationCoursesOthers: this.fb.control({ value: data.specializationCoursesOthers, disabled: true}, []),
      degree: this.fb.control({ value: data.degree, disabled: true }, []),
      lastQualifyCategory: this.fb.control(data.lastQualifyCategory, []),
      lastQualifyCategoryOrderSeries: this.fb.control({ value: data.lastQualifyCategoryOrderSeries, disabled: true }, []),
      lastQualifyCategoryOrderNumber: this.fb.control({ value: data.lastQualifyCategoryOrderNumber, disabled: true }, []),
      lastQualifyCategoryOrderDate: this.fb.control({ value: data.lastQualifyCategoryOrderDate, disabled: true }, []),
      treeYearsAgoMark: this.fb.control({ value: data.treeYearsAgoMark, disabled: true }, []),
      twoYearsAgoMark: this.fb.control({ value: data.twoYearsAgoMark, disabled: true }, []),
      lastYearMark: this.fb.control({ value: data.lastYearMark, disabled: true }, []),
      partialEvaluationFromDate: this.fb.control({ value: data.partialEvaluationFromDate, disabled: true }, []),
      partialEvaluationToDate: this.fb.control({ value: data.partialEvaluationToDate, disabled: true }, []),
      partialEvaluationRate: this.fb.control({ value: data.partialEvaluationRate, disabled: true }, []),
      partialEvaluationMark: this.fb.control({ value: data.partialEvaluationMark, disabled: true }, []),
      sanctionStartDate: this.fb.control({ value: data.sanctionStartDate, disabled: true }, []),
      sanctionEndDate: this.fb.control({ value: data.sanctionEndDate, disabled: true }, []),
      question1SelfEvaluationMark: this.fb.control({ value: data.question1SelfEvaluationMark, disabled: true }, []),
      question1Mark: this.fb.control({ value: data.question1Mark, disabled: true }, []),
      question2SelfEvaluationMark: this.fb.control({ value: data.question2SelfEvaluationMark, disabled: true }, []),
      question2Mark: this.fb.control({ value: data.question2Mark, disabled: true }, []),
      question3SelfEvaluationMark: this.fb.control({ value: data.question3SelfEvaluationMark, disabled: true }, []),
      question3Mark: this.fb.control({ value: data.question3Mark, disabled: true }, []),
      question4SelfEvaluationMark: this.fb.control({ value: data.question4SelfEvaluationMark, disabled: true }, []),
      question4Mark: this.fb.control({ value: data.question4Mark, disabled: true }, []),
      question5SelfEvaluationMark: this.fb.control({ value: data.question5SelfEvaluationMark, disabled: true }, []),
      question5Mark: this.fb.control({ value: data.question5Mark, disabled: true }, []),
      question6SelfEvaluationMark: this.fb.control({ value: data.question6SelfEvaluationMark, disabled: true }, []),
      question6Mark: this.fb.control({ value: data.question6Mark, disabled: true }, []),
      question7SelfEvaluationMark: this.fb.control({ value: data.question7SelfEvaluationMark, disabled: true }, []),
      question7Mark: this.fb.control({ value: data.question7Mark, disabled: true }, []),
      question8SelfEvaluationMark: this.fb.control({ value: data.question8SelfEvaluationMark, disabled: true }, []),
      question8Mark: this.fb.control({ value: data.question8Mark, disabled: true }, []),
      question9SelfEvaluationMark: this.fb.control({ value: data.question9SelfEvaluationMark, disabled: true }, []),
      question9Mark: this.fb.control({ value: data.question9Mark, disabled: true }, []),
      question10SelfEvaluationMark: this.fb.control({ value: data.question10SelfEvaluationMark, disabled: true }, []),
      question10Mark: this.fb.control({ value: data.question10Mark, disabled: true }, []),
      question11SelfEvaluationMark: this.fb.control({ value: data.question11SelfEvaluationMark, disabled: true }, []),
      question11Mark: this.fb.control({ value: data.question11Mark, disabled: true }, []),
      question12SelfEvaluationMark: this.fb.control({ value: data.question12SelfEvaluationMark, disabled: true }, []),
      question12Mark: this.fb.control({ value: data.question12Mark, disabled: true }, []),
      question13SelfEvaluationMark: this.fb.control({ value: data.question13SelfEvaluationMark, disabled: true }, []),
      question13Mark: this.fb.control({ value: data.question13Mark, disabled: true }, []),
      question14SelfEvaluationMark: this.fb.control({ value: data.question14SelfEvaluationMark, disabled: true }, []),
      question14Mark: this.fb.control({ value: data.question14Mark, disabled: true }, []),
      question15SelfEvaluationMark: this.fb.control({ value: data.question15SelfEvaluationMark, disabled: true }, []),
      question15Mark: this.fb.control({ value: data.question15Mark, disabled: true }, []),
      
      interviewDate: this.fb.control({ value: data.interviewDate, disabled: true }, []),
      comments: this.fb.control({ value: data.comments, disabled: true }, []),
      individualObjective1: this.fb.control({ value: data.individualObjective1, disabled: true }, []),
      individualObjective2: this.fb.control({ value: data.individualObjective2, disabled: true }, []),
      individualObjective3: this.fb.control({ value: data.individualObjective3, disabled: true }, []),
      needImprovement1: this.fb.control({ value: data.needImprovement1, disabled: true }, []),
      needImprovement2: this.fb.control({ value: data.needImprovement2, disabled: true }, []),
      needImprovement3: this.fb.control({ value: data.needImprovement3, disabled: true }, []),
      evaluatedComments: this.fb.control({ value: data.evaluatedComments, disabled: true }, []),
      evaluatedAcceptance: this.fb.control({ value: data.evaluatedAcceptance, disabled: true }, []),
      acceptanceDate: this.fb.control({ value: data.acceptanceDate, disabled: true }, []),
      evaluatedSigned: this.fb.control({ value: data.evaluatedSigned, disabled: true }, []),
      evaluatingFullName: this.fb.control({ value: data.evaluatingFullName, disabled: true }, []),
      evaluatingPosition: this.fb.control({ value: data.evaluatingPosition, disabled: true }, []),
      evaluatingDate: this.fb.control({ value: data.evaluatingDate, disabled: true }, []),
      evaluatingSigned: this.fb.control({ value: data.evaluatingSigned, disabled: true }, []),
      counterSingerComments: this.fb.control({ value: data.counterSingerComments, disabled: true }, []),
      counterSignerAcceptance: this.fb.control({ value: data.counterSignerAcceptance, disabled: true }, []),
      counterSignerName: this.fb.control({ value: data.counterSignerName, disabled: true }, []),
      counterSignerPosition: this.fb.control({ value: data.counterSignerPosition, disabled: true }, []),
      counterSignerSignDate: this.fb.control({ value: data.counterSignerSignDate, disabled: true }, []),
      counterSignerSigned: this.fb.control({ value: data.counterSignerSigned, disabled: true }, []),
      finalMark: this.fb.control({ value: data.finalMark, disabled: true }, [])
    });
  }

  submit(evaluatedAcceptance: number): void {
    this.surveyService.accept(this.id, { evaluatedAcceptance }).subscribe(response => {
      this.notificationService.success('Succes', 'Fisa a fost procesata cu succces!', NotificationUtil.getDefaultMidConfig());
      this.ngZone.run(() => this.router.navigate(['../../'], { relativeTo: this.route }));
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

}
