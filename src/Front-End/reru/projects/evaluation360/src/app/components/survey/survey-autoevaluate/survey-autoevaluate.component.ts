import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EvaluationService } from '../../../utils/services/survey.service';

@Component({
  selector: 'app-survey-autoevaluate',
  templateUrl: './survey-autoevaluate.component.html',
  styleUrls: ['./survey-autoevaluate.component.scss']
})
export class SurveyAutoevaluateComponent implements OnInit {
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
              private evaluationService: EvaluationService,
              private route: ActivatedRoute,
              private router: Router,
              private notificationService: NotificationsService,
              private ngZone: NgZone) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.id = +response.id;
        this.retrieveAutoevaluate(+response.id);
      } else {
        this.router.navigate(['../../'], { relativeTo: this.route });
      }
    });
  }

  retrieveAutoevaluate(id: number): void {
    this.evaluationService.retrieveAutoevaluate(id).subscribe(response => {
      this.initForm(response);
      this.isLoading = false;
    });
  }

  initForm(data: any = {}): void {
    this.surveyForm = this.fb.group({
      question1Mark: this.fb.control({ value: data.question1Mark, disabled: true }, []),
      question1SelfEvaluationMark: this.fb.control(data.question1SelfEvaluationMark, []),
      question2Mark: this.fb.control({ value: data.question2Mark, disabled: true }, []),
      question2SelfEvaluationMark: this.fb.control(data.question2SelfEvaluationMark, []),
      question3Mark: this.fb.control({ value: data.question3Mark, disabled: true }, []),
      question3SelfEvaluationMark: this.fb.control(data.question3SelfEvaluationMark, []),
      question4Mark: this.fb.control({ value: data.question4Mark, disabled: true }, []),
      question4SelfEvaluationMark: this.fb.control(data.question4SelfEvaluationMark, []),
      question5Mark: this.fb.control({ value: data.question5Mark, disabled: true }, []),
      question5SelfEvaluationMark: this.fb.control(data.question5SelfEvaluationMark, []),
      question6Mark: this.fb.control({ value: data.question6Mark, disabled: true }, []),
      question6SelfEvaluationMark: this.fb.control(data.question6SelfEvaluationMark, []),
      question7Mark: this.fb.control({ value: data.question7Mark, disabled: true }, []),
      question7SelfEvaluationMark: this.fb.control(data.question7SelfEvaluationMark, []),
      question8Mark: this.fb.control({ value: data.question8Mark, disabled: true }, []),
      question8SelfEvaluationMark: this.fb.control(data.question8SelfEvaluationMark, []),
      question9Mark: this.fb.control({ value: data.question9Mark, disabled: true }, []),
      question9SelfEvaluationMark: this.fb.control(data.question9SelfEvaluationMark, []),
      question10Mark: this.fb.control({ value: data.question10Mark, disabled: true }, []),
      question10SelfEvaluationMark: this.fb.control(data.question10SelfEvaluationMark, []),
      question11Mark: this.fb.control({ value: data.question11Mark, disabled: true }, []),
      question11SelfEvaluationMark: this.fb.control(data.question11SelfEvaluationMark, []),
      question12Mark: this.fb.control({ value: data.question12Mark, disabled: true }, []),
      question12SelfEvaluationMark: this.fb.control(data.question12SelfEvaluationMark, []),
      question13Mark: this.fb.control({ value: data.question13Mark, disabled: true }, []),
      question13SelfEvaluationMark: this.fb.control(data.question13SelfEvaluationMark, []),
      question14Mark: this.fb.control({ value: data.question14Mark, disabled: true }, []),
      question14SelfEvaluationMark: this.fb.control(data.question14SelfEvaluationMark, []),
      question15Mark: this.fb.control({ value: data.question15Mark, disabled: true }, []),
      question15SelfEvaluationMark: this.fb.control(data.question15SelfEvaluationMark, []),
      evaluatedComments: this.fb.control(data.evaluatedComments, [])
    });
  }

  submit(): void {
    this.evaluationService.autoevaluate(this.id, this.surveyForm.value).subscribe(response => {
      this.notificationService.success('Succes', 'Fisa a fost transmisa cu succes!', NotificationUtil.getDefaultMidConfig());
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
