import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteStudyModalComponent } from 'projects/personal/src/app/utils/modals/delete-study-modal/delete-study-modal.component';
import { EditStydyModalComponent } from 'projects/personal/src/app/utils/modals/edit-stydy-modal/edit-stydy-modal.component';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { StudyModel } from 'projects/personal/src/app/utils/models/study.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { StudyService } from 'projects/personal/src/app/utils/services/study.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ContractorParser } from '../../add/add.parser';
import { forkJoin, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ModernLanguageLevelModel } from 'projects/personal/src/app/utils/models/modern-language-level.model';
import { RecommendationForStudyModel } from 'projects/personal/src/app/utils/models/recommendation-for-study.model';
import { StudyEnum } from 'projects/personal/src/app/utils/models/study.enum';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../data.service';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { RecommendationForStudyService } from 'projects/personal/src/app/utils/services/recommendation-for-study.service';
import { ModernLanguageLevelService } from 'projects/personal/src/app/utils/services/modern-language-level.service';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { I18nService } from '../../../../utils/services/i18n.service';


@Component({
  selector: 'app-study-details',
  templateUrl: './study-details.component.html',
  styleUrls: ['./study-details.component.scss']
})
export class StudyDetailsComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractor: Contractor;

  isLoading: boolean = true;
  isLoadingStudies: boolean = true;
  isLoadingStudiesContent: boolean = false;
  isLoadingLanguages: boolean = true;
  isLoadingRecommandations: boolean = true;

  studyForm: FormGroup;
  languageForm: FormGroup;
  recommendationForm: FormGroup;
  panelOpenState: boolean = true;

  studyTypes;
  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{ label: '', value: '' }];

  studyFrequences;
  stepId;
  contractorId;
  userGeneralData;
  registrationFluxStep;

  userStudies: StudyModel[];
  userModernLanguages: ModernLanguageLevelModel[];
  recommendationForStudy: RecommendationForStudyModel[];

  modernLanguages;
  knowledgeQuelifiersEnum;
  studyProfilesEnum;
  studyCoursesEnum;

  addOrEditStudyButton: boolean;
  addOrEditLanguagesButton: boolean;
  addOrEditRecommendationsButton: boolean;
  isDone: boolean;

  title: string;
  description: string;

  pagedSummary: PagedSummary = new PagedSummary();

  constructor(private fb: FormBuilder,
              private studyService: StudyService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private referenceService: ReferenceService,
              private route: ActivatedRoute,
              private ds: DataService,
              private recommendationForStudyService: RecommendationForStudyService,
              private modernLanguageLevelService: ModernLanguageLevelService,
              private registrationFluxService: RegistrationFluxStepService,
              private contractorService: ContractorService,
              public translate: I18nService
    ) { }

  ngOnInit(): void {
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.subscribeForParams();
    // this.subscribeForStudies();
    this.retrieveDropdowns();
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  subscribeForParams(): void {
      this.getUser(this.contractorId);

      this.getExistentStep(this.stepId, this.contractor.id);
      this.isLoading = false;
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.contractor = response.data;
      this.subscribeForStudies(response.data);
    });
  }

  initForm(data?, studyType?: StudyEnum): void {

    if (data == null) {

      this.studyForm = this.fb.group({
        studies: this.fb.array([this.generateStudies()]),
      });

      this.languageForm = this.fb.group({
        modernLanguages: this.fb.array([this.generateModernLanguage()])
      });

      this.recommendationForm = this.fb.group({
        recommendation: this.fb.array([this.generateRecommendation()])
      });

    }
    else if (studyType == StudyEnum.Studies) {

      this.studyForm = this.fb.group({
        studies: this.fb.array([this.generateStudies(data, this.contractorId)]),
      });

    }
    else if (studyType == StudyEnum.ModernLanguageLevels) {

      this.languageForm = this.fb.group({
        modernLanguages: this.fb.array([this.generateModernLanguage(data, data.modernLanguageId, this.contractorId)])
      });
    }
    else if (studyType == StudyEnum.Recommandations) {

      this.recommendationForm = this.fb.group({
        recommendation: this.fb.array([this.generateRecommendation(data, this.contractorId)])
      });
    }
  }

  retrieveDropdowns(): void {
    this.referenceService.getStudyFrequencyEnum().subscribe(res => {
      this.studyFrequences = res.data;
    });

    this.referenceService.getStudyTypes().subscribe(res => {
      let types = res.data;
      this.studyTypes = types.sort(function (a, b) { return a.translateId - b.translateId });
    });

    this.referenceService.getModernLanguages().subscribe(res => {
      this.modernLanguages = res.data
    });

    this.referenceService.getKnowledgeQuelifiersEnum().subscribe(res => {
      this.knowledgeQuelifiersEnum = res.data
    });

    this.referenceService.getStudyCoursesEnum().subscribe(res => {
      this.studyCoursesEnum = res.data
    });

    this.referenceService.getStudyProfilesEnum().subscribe(res => {
      this.studyProfilesEnum = res.data
    });
  }

  subscribeForStudies(contractor) {
      if (contractor.hasStudies) {

        this.getUserStudies(contractor.id);
      }
      else {
        this.addOrEditStudyButton = false;
        this.isLoadingStudies = false;
        this.userStudies = null;
      }

      if (contractor.hasModernLanguages) {

        this.getUserModernLanguages(contractor.id);
      }
      else {
        this.addOrEditLanguagesButton = false;
        this.isLoadingLanguages = false;
        this.userModernLanguages = null;
      }

      if (contractor.hasRecommendationsForStudy) {

          this.getUserRecommendation(contractor.id);
        }
        else {
          this.addOrEditRecommendationsButton = false;
          this.isLoadingRecommandations = false;
          this.recommendationForStudy = null;
        }
  }

  getUserStudies(contractorId) {
    this.isLoadingStudies = true;

    const request = {
      contractorId: contractorId
    }
    this.studyService.list(request).subscribe(res => {
      this.userStudies = res.data.items;
      this.initExistentStudyForm(this.userStudies);
      this.isLoadingStudies = false;
      this.addOrEditStudyButton = true;
    })
  }

  getUserModernLanguages(contractorId) {
    this.isLoadingLanguages = true;

    const request = {
      contractorId: contractorId
    }
    this.modernLanguageLevelService.list(request).subscribe(res => {
      this.userModernLanguages = res.data.items;
      this.initExistentLanguageForm(this.userModernLanguages);
      this.isLoadingLanguages = false;
      this.addOrEditLanguagesButton = true;
    })
  }

  getUserRecommendation(contractorId) {
    this.isLoadingRecommandations = true;

    const request = {
      contractorId: contractorId
    }

    this.recommendationForStudyService.list(request).subscribe(res => {
      this.recommendationForStudy = res.data.items;
      this.initRecommandationForm(this.recommendationForStudy);
      this.isLoadingRecommandations = false;
      this.addOrEditRecommendationsButton = true;
    })
  }

  initExistentStudyForm(study) {
    
    if (study != null) {

      for (let i = 0; i < study.length; i++) {

        if (i > 0) {
          this.addStudy(study[i])
        }else{
          this.initForm(study[i], StudyEnum.Studies)
        }
      }

    }

  }

  initExistentLanguageForm(language) {
    
    if (language != null) {

      for (let i = 0; i < language.length; i++) {

        if (i > 0) {
          this.addModernLanguage(language[i])
        }else{
          this.initForm(language[i], StudyEnum.ModernLanguageLevels)
        }
      }
    }
  }

  initRecommandationForm(recommendations) {
    
    if (recommendations != null) {

      for (let i = 0; i < recommendations.length; i++) {

        if (i > 0) {
          this.addRecommandation(recommendations[i])
        }else{
          this.initForm(recommendations[i], StudyEnum.Recommandations)
        }
      }
    }
  }

  generateStudies(study?, contractorId?): FormGroup {
    return this.fb.group({
      id: this.fb.control((study && study.id) || null, []),
      institution: this.fb.control((study && study.institution) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      institutionAddress: this.fb.control((study && study.institutionAddress) || null, [Validators.required]),
      studyTypeId: this.fb.control((study && study.studyTypeId) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyFrequency: this.fb.control((study && study.studyFrequency) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyProfile: this.fb.control((study && study.studyProfile) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyCourse: this.fb.control((study && study.studyCourse) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      faculty: this.fb.control((study && study.faculty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\- ]+$/)]),
      specialty: this.fb.control((study && study.specialty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      yearOfAdmission: this.fb.control((study && study.yearOfAdmission) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      graduationYear: this.fb.control((study && study.graduationYear) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      startStudyPeriod: this.fb.control((study && study.startStudyPeriod) || null, [Validators.required]),
      endStudyPeriod: this.fb.control((study && study.endStudyPeriod) || null, [Validators.required]),
      title: this.fb.control((study && study.title) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      qualification: this.fb.control((study && study.qualification) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      creditCount: this.fb.control((study && study.creditCount) || null, [Validators.required]),
      studyActSeries: this.fb.control((study && study.studyActSeries) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      studyActNumber: this.fb.control((study && study.studyActNumber) || null, [Validators.required, Validators.pattern(/^[0-9]+(\.?[0-9]+)?$/)]),
      studyActRelaseDay: this.fb.control((study && study.studyActRelaseDay) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, [])
    });
  }

  generateModernLanguage(language?, modernLanguageId?, contractorId?) {
    return this.fb.group({
      id: this.fb.control((language && language.id) || null, []),
      knowledgeQuelifiers: this.fb.control((language && language.knowledgeQuelifiers) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      contractorId: this.fb.control(contractorId || null, []),
      modernLanguageId: this.fb.control(modernLanguageId || null, [Validators.required]),
    });
  }

  generateRecommendation(recommendation?, contractorId?) {
    return this.fb.group({
      id: this.fb.control((recommendation && recommendation.id) || null, []),
      name: this.fb.control((recommendation && recommendation.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((recommendation && recommendation.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((recommendation && recommendation.function) || null, [Validators.required]),
      subdivision: this.fb.control((recommendation && recommendation.subdivision) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, []),
    });
  }

  isInvalidPattern(form, field: string): boolean {
    return ValidatorUtil.isInvalidPattern(form, field);
  }

  removeModernLanguage(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.languageForm.controls.modernLanguages).controls.splice(index, 1);
  }

  removeStudy(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.studyForm.controls.studies).controls.splice(index, 1);
  }

  removeRecommendation(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.recommendationForm.controls.recommendation).controls.splice(index, 1);
  }

  createStudies(): void {
    this.buildStudiesForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-study-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserStudies(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-study-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  createModernLanguage() {
    this.buildModernLanguageForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-modern-language-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserModernLanguages(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-modern-language-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  createRecommendationForStudy() {
    this.buildReommendationForStudyForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-study-recommendation-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserRecommendation(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-study-recommendation-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  buildStudiesForm(): Observable<any> {
    const request = this.parseStudies(this.studyForm.getRawValue().studies, this.contractorId);
    return this.studyService.addMultiple(request);
  }

  buildModernLanguageForm(): Observable<any> {
    const request = this.parseModernLanguages(this.languageForm.getRawValue().modernLanguages, this.contractorId);
    return this.modernLanguageLevelService.addMultiple(request);
  }

  buildReommendationForStudyForm(): Observable<any> {
    const request = this.parseRecommendationsForStudy(this.recommendationForm.getRawValue().recommendation, this.contractorId);
    return this.recommendationForStudyService.addMultiple(request);
  }

  parseStudies(data: StudyModel[], contractorId: number): StudyModel[] {
    return data.map(el => this.parseStudy(el, contractorId));
  }

  parseModernLanguages(data: ModernLanguageLevelModel[], contractorId: number): ModernLanguageLevelModel[] {
    return data.map(el => this.parseModernLanguage(el, contractorId));
  }

  parseRecommendationsForStudy(data: RecommendationForStudyModel[], contractorId: number): RecommendationForStudyModel[] {
    return data.map(el => this.parseRecomendationForStudy(el, contractorId));
  }

  parseStudy(data: StudyModel, contractorId): StudyModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      institution: data.institution,
      studyFrequency: data.studyFrequency,
      studyTypeId: data.studyTypeId,
      institutionAddress: data.institutionAddress,
      faculty: data.faculty,
      specialty: data.specialty,
      yearOfAdmission: data.yearOfAdmission,
      graduationYear: data.graduationYear,
      contractorId: contractorId,
      studyCourse: data.studyCourse,
      startStudyPeriod: data.startStudyPeriod,
      endStudyPeriod: data.endStudyPeriod,
      title: data.title,
      studyProfile: data.studyProfile,
      qualification: data.qualification,
      creditCount: data.creditCount,
      studyActSeries: data.studyActSeries,
      studyActNumber: data.studyActNumber,
      studyActRelaseDay: data.studyActRelaseDay
    })
  }

  parseModernLanguage(data: ModernLanguageLevelModel, contractorId): ModernLanguageLevelModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      modernLanguageId: data.modernLanguageId,
      knowledgeQuelifiers: data.knowledgeQuelifiers,
      contractorId: contractorId
    })
  }

  parseRecomendationForStudy(data: RecommendationForStudyModel, contractorId): RecommendationForStudyModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      function: data.function,
      subdivision: data.subdivision,
      contractorId: contractorId
    })
  }

  addStudy(study?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (study == null) {
      (<FormArray>this.studyForm.controls.studies).controls.push(this.generateStudies());
    } else {
      (<FormArray>this.studyForm.controls.studies).controls.push(
        this.generateStudies(
          study,
          this.contractorId
        )
      );
    }
  }

  addModernLanguage(language?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (language == null) {
      (<FormArray>this.languageForm.controls.modernLanguages).controls.push(this.generateModernLanguage());
    } else {
      (<FormArray>this.languageForm.controls.modernLanguages).controls.push(
        this.generateModernLanguage(
          language,
          language.modernLanguageId,
          this.contractorId
        )
      );
    }
  }

  addRecommandation(recommendation?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (recommendation == null) {
      (<FormArray>this.recommendationForm.controls.recommendation).controls.push(this.generateRecommendation());
    } else {
      (<FormArray>this.recommendationForm.controls.recommendation).controls.push(
        this.generateRecommendation(
          recommendation,
          this.contractorId
        )
      );
    }
  }

  getExistentStep(step, contractorId) {
    const request = {
      contractorId: contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep() {
    if (this.userStudies != null || this.userModernLanguages != null || this.recommendationForStudy != null) {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractorId);

    }
    else {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractorId);
    }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId) {
    const datas = {
      isDone: success,
      stepId: this.stepId
    }

    if (stepData.length == 0) {
      this.addCandidateRegistationStep(success, stepId, contractorId);
      this.ds.sendData(datas);
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId) {
    const request = {
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }
    this.registrationFluxService.add(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId) {
    const request = {
      id: id,
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }

    this.registrationFluxService.update(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-erorr'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  studyTypeFieldsValidator(index) {
    this.isLoadingStudiesContent =true;
    const studies = this.studyForm.controls.studies as FormArray;
    let study = studies.controls[index];

    let studyType = this.studyTypes?.filter(el => el.value == study.value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 1 || studyType[0].validationId == 2 || studyType[0].validationId == 3) {
        study.get("title").setValue(null);
        study.get("studyFrequency").setValue(null);
        study.get("studyCourse").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
        study.get("startStudyPeriod").setValue(null);
        study.get("specialty").setValue(null);
        study.get("qualification").setValue(null);
        study.get("faculty").setValue(null);
        study.get("endStudyPeriod").setValue(null);
        study.get("creditCount").setValue(null);
      } else if (studyType[0].validationId == 4 || studyType[0].validationId == 5) {
        study.get("studyCourse").setValue(null);
        study.get("studyProfile").setValue(null);
        study.get("title").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
        study.get("startStudyPeriod").setValue(null);
        study.get("faculty").setValue(null);
        study.get("endStudyPeriod").setValue(null);
        study.get("creditCount").setValue(null);
      } else if (studyType[0].validationId == 6) {
        study.get("studyCourse").setValue(null);
        study.get("studyProfile").setValue(null);
        study.get("title").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
        study.get("startStudyPeriod").setValue(null);
        study.get("endStudyPeriod").setValue(null);
        study.get("creditCount").setValue(null);
      } else if (studyType[0].validationId == 7 || studyType[0].validationId == 8) {
        study.get("studyCourse").setValue(null);
        study.get("studyProfile").setValue(null);
        study.get("startStudyPeriod").setValue(null);
        study.get("endStudyPeriod").setValue(null);
        study.get("qualification").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
        study.get("creditCount").setValue(null);
      } else if (studyType[0].validationId == 9 || studyType[0].validationId == 10) {
        study.get("creditCount").setValue(null);
        study.get("endStudyPeriod").setValue(null);
        study.get("faculty").setValue(null);
        study.get("qualification").setValue(null);
        study.get("startStudyPeriod").setValue(null);
        study.get("studyCourse").setValue(null);
        study.get("studyProfile").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
      } else if (studyType[0].validationId == 11) {
        study.get("faculty").setValue(null);
        study.get("graduationYear").setValue(null);
        study.get("specialty").setValue(null);
        study.get("studyFrequency").setValue(null);
        study.get("studyProfile").setValue(null);
        study.get("title").setValue(null);
        study.get("yearOfAdmission").setValue(null);
        study.get("studyActRelaseDay").setValue(null);
      }
    }
    setTimeout(() => {
    this.isLoadingStudiesContent =false;
    }, 100)
  }

  studyButtonValidator(study) {
    let results: boolean[] = [];
    
    for (let i = 0; i < study.length; i++) {

      let studyType = this.studyTypes?.filter(el => el.value == study[i].value.studyTypeId);
      if (studyType?.length > 0) {
        if (studyType[0].validationId == 1 || studyType[0].validationId == 2 || studyType[0].validationId == 3) {
          results.push(!(
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.yearOfAdmission && !ValidatorUtil.isInvalidPattern(study[i], "yearOfAdmission")) &&
            (study[i].value.graduationYear && !ValidatorUtil.isInvalidPattern(study[i], "graduationYear")) &&
            (study[i].value.studyProfile && !ValidatorUtil.isInvalidPattern(study[i], "studyProfile"))
          ))
            
        } else if (studyType[0].validationId == 4 || studyType[0].validationId == 5) {
          results.push(!(
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.studyFrequency && !ValidatorUtil.isInvalidPattern(study[i], "studyFrequency")) &&
            (study[i].value.specialty && !ValidatorUtil.isInvalidPattern(study[i], "specialty")) &&
            (study[i].value.qualification && !ValidatorUtil.isInvalidPattern(study[i], "qualification")) &&
            (study[i].value.yearOfAdmission && !ValidatorUtil.isInvalidPattern(study[i], "yearOfAdmission")) &&
            (study[i].value.graduationYear && !ValidatorUtil.isInvalidPattern(study[i], "graduationYear")) &&
            (study[i].value.studyActSeries && !ValidatorUtil.isInvalidPattern(study[i], "studyActSeries")) &&
            (study[i].value.studyActNumber && !ValidatorUtil.isInvalidPattern(study[i], "studyActNumber")) &&
            (study[i].value.studyActRelaseDay && !ValidatorUtil.isInvalidPattern(study[i], "studyActRelaseDay"))
          ))
        } else if (studyType[0].validationId == 6) {
          results.push(!(
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.studyFrequency && !ValidatorUtil.isInvalidPattern(study[i], "studyFrequency")) &&
            (study[i].value.faculty && !ValidatorUtil.isInvalidPattern(study[i], "faculty")) &&
            (study[i].value.qualification && !ValidatorUtil.isInvalidPattern(study[i], "qualification")) &&
            (study[i].value.specialty && !ValidatorUtil.isInvalidPattern(study[i], "specialty")) &&
            (study[i].value.yearOfAdmission && !ValidatorUtil.isInvalidPattern(study[i], "yearOfAdmission")) &&
            (study[i].value.graduationYear && !ValidatorUtil.isInvalidPattern(study[i], "graduationYear")) &&
            (study[i].value.studyActSeries && !ValidatorUtil.isInvalidPattern(study[i], "studyActSeries")) &&
            (study[i].value.studyActNumber && !ValidatorUtil.isInvalidPattern(study[i], "studyActNumber")) &&
            (study[i].value.studyActRelaseDay && !ValidatorUtil.isInvalidPattern(study[i], "studyActRelaseDay"))
          ))
            
        } else if (studyType[0].validationId == 7 || studyType[0].validationId == 8) {
          results.push(!(
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.studyFrequency && !ValidatorUtil.isInvalidPattern(study[i], "studyFrequency")) &&
            (study[i].value.faculty && !ValidatorUtil.isInvalidPattern(study[i], "faculty")) &&
            (study[i].value.title && !ValidatorUtil.isInvalidPattern(study[i], "title")) &&
            (study[i].value.specialty && !ValidatorUtil.isInvalidPattern(study[i], "specialty")) &&
            (study[i].value.yearOfAdmission && !ValidatorUtil.isInvalidPattern(study[i], "yearOfAdmission")) &&
            (study[i].value.graduationYear && !ValidatorUtil.isInvalidPattern(study[i], "graduationYear")) &&
            (study[i].value.studyActSeries && !ValidatorUtil.isInvalidPattern(study[i], "studyActSeries")) &&
            (study[i].value.studyActNumber && !ValidatorUtil.isInvalidPattern(study[i], "studyActNumber")) &&
            (study[i].value.studyActRelaseDay && !ValidatorUtil.isInvalidPattern(study[i], "studyActRelaseDay"))
          ))
        } else if (studyType[0].validationId == 9 || studyType[0].validationId == 10) {
          results.push(!(
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.studyFrequency && !ValidatorUtil.isInvalidPattern(study[i], "studyFrequency")) &&
            (study[i].value.title && !ValidatorUtil.isInvalidPattern(study[i], "title")) &&
            (study[i].value.specialty && !ValidatorUtil.isInvalidPattern(study[i], "specialty")) &&
            (study[i].value.yearOfAdmission && !ValidatorUtil.isInvalidPattern(study[i], "yearOfAdmission")) &&
            (study[i].value.graduationYear && !ValidatorUtil.isInvalidPattern(study[i], "graduationYear")) &&
            (study[i].value.studyActSeries && !ValidatorUtil.isInvalidPattern(study[i], "studyActSeries")) &&
            (study[i].value.studyActNumber && !ValidatorUtil.isInvalidPattern(study[i], "studyActNumber")) &&
            (study[i].value.studyActRelaseDay && !ValidatorUtil.isInvalidPattern(study[i], "studyActRelaseDay"))
          ))
        } else if (studyType[0].validationId == 11) {
          results.push(!(
            (study[i].value.studyCourse && !ValidatorUtil.isInvalidPattern(study[i], "studyCourse")) &&
            (study[i].value.institution && !ValidatorUtil.isInvalidPattern(study[i], "institution")) &&
            (study[i].value.studyTypeId && !ValidatorUtil.isInvalidPattern(study[i], "studyTypeId")) &&
            (study[i].value.creditCount && !ValidatorUtil.isInvalidPattern(study[i], "creditCount")) &&
            (study[i].value.qualification && !ValidatorUtil.isInvalidPattern(study[i], "qualification")) &&
            (study[i].value.startStudyPeriod && !ValidatorUtil.isInvalidPattern(study[i], "startStudyPeriod")) &&
            (study[i].value.endStudyPeriod && !ValidatorUtil.isInvalidPattern(study[i], "endStudyPeriod")) &&
            (study[i].value.studyActSeries && !ValidatorUtil.isInvalidPattern(study[i], "studyActSeries")) &&
            (study[i].value.studyActNumber && !ValidatorUtil.isInvalidPattern(study[i], "studyActNumber")) &&
            (study[i].value.studyActRelaseDay && !ValidatorUtil.isInvalidPattern(study[i], "studyActRelaseDay"))
          ))
        }
      }else{
        results.push(true);
      }
    }
    return results.some((x) => x == true) ? true : false;
  }

  enableFirstStudyTypeForm(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 1 || studyType[0].validationId == 2 || studyType[0].validationId == 3) {
        return true
      }
    }
    return false;
  }

  enableSecondStudyTypeForm(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 4 || studyType[0].validationId == 5 || studyType[0].validationId == 9 || studyType[0].validationId == 10) {
        return true
      }
    }
    return false;
  }

  enableThirdStudyTypeForm(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 6 || studyType[0].validationId == 7 || studyType[0].validationId == 8) {
        return true
      }
    }
    return false;
  }

  enableFourthStudyTypeForm(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 11) {
        return true
      }
    }
    return false;
  }

  disableThirdStudyTypeField(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 6) {
        return true
      }
    }
    return false;
  }

  disableMasterStudyTypeField(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 7) {
        return true
      }
    }
    return false;
  }

  disableSecondStudyTypeField(value) {
    let studyType = this.studyTypes?.filter(el => el.value == value.studyTypeId);

    if (studyType?.length > 0) {
      if (studyType[0].validationId == 4 || studyType[0].validationId == 5) {
        return true
      }
    }
    return false;
  }

  validateCreateStudyButton() {
    let result: boolean;

    for (let i = 0; i < this.studyForm.value.studies.length; i++) {
      result = (this.studyForm.value.studies[i].institution &&
        this.studyForm.value.studies[i].studyTypeId &&
        this.studyForm.value.studies[i].studyFrequency &&
        this.studyForm.value.studies[i].faculty &&
        this.studyForm.value.studies[i].specialty) == null
    }

    return result;
  }

  isLengthValidator(form, field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(form, field);
  }

  inputValidator(form, field) {
    return !ValidatorUtil.isInvalidPattern(form, field) && form.get(field).valid ? 'is-valid' : 'is-invalid';
  }

  // retrieveDropdowns(): void {
  //   this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 5 }).subscribe(response => {
  //     this.studyTypes = response.data;

  //     this.retrieveStudies();

  //     this.isLoading = false;
  //   });
  // }

  // retrieveStudies(data: any = {}): void {
  //   const request = {
  //     contractorId: this.contractor.id,
  //     page: data.page || this.pagedSummary.currentPage,
  //     itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
  //   }
  //   this.studyService.get(request).subscribe((response: ApiResponse<any>) => {
  //     this.studies = [...response.data.items];
  //     this.pagedSummary = response.data.pagedSummary;
  //     this.initForm(response.data.items);
  //   });
  // }

  // initForm(studies: StudyModel[]): void {
  //   studies.forEach((element, index) => {
  //     this.selectedItems[index] = this.studyTypes && this.studyTypes.find(el => +el.value === element.studyTypeId);
  //   });

  //   this.studyForm = this.fb.group({
  //     studies: this.fb.array(this.buildStudies(studies))
  //   });
  //   this.isLoading = false;
  // }

  // buildStudies(studies): StudyModel[] {
  //   return studies.map((el: StudyModel) => this.generateStudy(el));
  // }

  // searchStudyType(index, text$) {
  //   return (text$: Observable<string>) => {
  //     const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
  //     const clicksWithClosedPopup$ = this.click$[index].pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
  //     const inputFocus$ = this.focus$[index];

  //     return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
  //       map(term => (term === '' ? this.studyTypes
  //         : this.studyTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  //   }
  // }

  // selectStudyType(studyType: SelectItem, index:number): void {
  //   (<FormArray>this.studyForm.controls.studies).controls[index].get('studyTypeId').patchValue(studyType ? +studyType.value : null);
  // }

  // generateStudy(study: StudyModel): FormGroup {
  //   return this.fb.group({
  //     id: this.fb.control(study.id),
  //     institution: this.fb.control({ value: study.institution, disabled: true }, [Validators.required]),
  //     studyFrequency: this.fb.control({ value: study.studyFrequency, disabled: true }, [Validators.required]),
  //     faculty: this.fb.control({ value: study.faculty, disabled: true }, [Validators.required]),
  //     qualification: this.fb.control({ value: study.qualification, disabled: true }, [Validators.required]),
  //     specialty: this.fb.control({ value: study.specialty, disabled: true }, [Validators.required]),
  //     diplomaNumber: this.fb.control({ value: study.diplomaNumber, disabled: true }, [Validators.required]),
  //     diplomaReleaseDay: this.fb.control({ value: study.diplomaReleaseDay, disabled: true }, [Validators.required]),
  //     isActiveStudy: this.fb.control({ value: study.isActiveStudy, disabled: true }, [Validators.required]),
  //     contractorId: this.fb.control({ value: study.contractorId, disabled: true }, []),
  //     studyTypeId: this.fb.control({ value: study.studyTypeId, disabled: true }, [])
  //   });
  // }

  // openEditStudyModal(study: FormGroup, index: number): void {
  //   const modalRef = this.modalService.open(EditStydyModalComponent, { centered: true, backdrop: 'static', size: 'lg'});
  //   modalRef.componentInstance.study = {...study.getRawValue()};
  //   modalRef.result.then((response) => this.editStudy(response), () => {});
  // }

  // editStudy(data: StudyModel): void {
  //   this.isLoading = true;
  //   this.studyService.update(data).subscribe(response => {
  //     this.retrieveStudies();
  //     this.notificationService.success('Success', 'Study updated!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //     this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
  //   });
  // }

  // openDeleteStudyModal(id: number): void {
  //   const modalRef = this.modalService.open(DeleteStudyModalComponent, { centered: true, backdrop: 'static'});
  //   modalRef.result.then(() => this.deleteStudy(id), () => {});
  // }

  // deleteStudy(id: number): void {
  //   this.isLoading = true;
  //   this.studyService.delete(id).subscribe(response => {
  //     this.retrieveStudies();
  //     this.notificationService.success('Success', 'Study deleted!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //     this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
  //   });
  // }

  // openAddStudyModal(): void {
  //   const modalRef = this.modalService.open(EditStydyModalComponent, { centered: true, backdrop: 'static', size: 'lg'});
  //   modalRef.componentInstance.study = <StudyModel>{ contractorId: this.contractor.id };
  //   modalRef.componentInstance.study.studyFrequency = 0;
  //   modalRef.result.then((response) => this.addStudy(response), () => {});
  // }

  // addStudy(data: StudyModel): void {
  //   this.isLoading = true;
  //   this.studyService.add(ContractorParser.parseStudy(data, this.contractor.id)).subscribe(response => {
  //     this.retrieveStudies();
  //     this.notificationService.success('Success', 'Study added!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //     this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
  //   });
  // }

  // formatter = (x:SelectItem) => x.label;
}
