import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ReferenceService } from '../../../utils/services/reference.service';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ValidatorUtil } from '../../../utils/util/validator.util';
import { StudyModel } from '../../../utils/models/study.model';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { ActivatedRoute, Router } from '@angular/router';
import { StudyService } from '../../../utils/services/study.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { ModernLanguageLevelService } from '../../../utils/services/modern-language-level.service';
import { RecommendationForStudyService } from '../../../utils/services/recommendation-for-study.service';
import { StudyEnum } from '../../../utils/models/study.enum';
import { ModernLanguageLevelModel } from '../../../utils/models/modern-language-level.model';
import { RecommendationForStudyModel } from '../../../utils/models/recommendation-for-study.model';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { ProfileService } from '../../../utils/services/profile.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-study',
  templateUrl: './study.component.html',
  styleUrls: ['./study.component.scss']
})
export class StudyComponent implements OnInit {
  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  @Output() isDoneStep = new EventEmitter<{ isDone: boolean }>();
  
  isLoadingStudies: boolean = true;
  isLoadingLanguages: boolean = true;
  isLoadingRecommandations: boolean = true;

  studyForm: FormGroup;
  languageForm: FormGroup;
  recommendationForm: FormGroup;

  studyTypes: SelectItem[];
  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{ label: '', value: '' }];

  studyFrequences;
  userId;
  stepId;
  userGeneralData;
  registrationFluxStep;

  userStudies: StudyModel[];
  userModernLanguages: ModernLanguageLevelModel[];
  recommendationForStudy: RecommendationForStudyModel[];

  modernLanguages;
  knowledgeQuelifiersEnum;

  addOrEditStudyButton: boolean;
  addOrEditLanguagesButton: boolean;
  addOrEditRecommendationsButton: boolean;
  isDone: boolean;


  constructor(private referenceService: ReferenceService,
    private fb: FormBuilder,
    private notificationService: NotificationsService,
    private route: ActivatedRoute,
    private studyService: StudyService,
    private userProfile: UserProfileService,
    private modernLanguageLevelService: ModernLanguageLevelService,
    private recommendationForStudyService: RecommendationForStudyService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService

    ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);
    this.getUserStudies(this.userId);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());
    this.getExistentStep(this.stepId);

    this.initForm();
    this.getUserGeneralData();
    this.retrieveDropdowns();
  }
  
  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  initForm(data?, studyType?: StudyEnum): void {
    
    if(data == null){

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
    else if (studyType == StudyEnum.Studies ){

      this.studyForm = this.fb.group({
        studies: this.fb.array([this.generateStudies(data, this.userId)]),
      });

    }
    else if (studyType == StudyEnum.ModernLanguageLevels){
       
      this.languageForm = this.fb.group({
        modernLanguages: this.fb.array([this.generateModernLanguage(data, data.modernLanguage,  this.userId)])
      });
    }
    else if(studyType == StudyEnum.Recommandations){

      this.recommendationForm = this.fb.group({
        recommendation: this.fb.array([this.generateRecommendation(data, this.userId)])
      });
    } 
  }

  retrieveDropdowns(): void {
    this.referenceService.getStudyFrequencyEnum().subscribe(res => {
      this.studyFrequences = res.data;
    });

    this.referenceService.getStudyTypes().subscribe(res => {
      this.studyTypes = res.data;
    });

    this.referenceService.getModernLanguages().subscribe(res => {
      this.modernLanguages = res.data
    });

    this.referenceService.getKnowledgeQuelifiersEnum().subscribe(res => {
      this.knowledgeQuelifiersEnum = res.data
    });
  }

  getUserGeneralData() {
    this.userProfile.getCandidateProfile(this.userId).subscribe( res => {

      this.userGeneralData = res.data;
      const userData = res.data;

      if (userData.studyCount != 0) {

        this.getUserStudies(this.userGeneralData.id);
      }
      else {
        this.addOrEditStudyButton = false;
        this.isLoadingStudies = false;
        this.userStudies = null;
      }

      if (userData.modernLanguageLevelsCount != 0) {

        this.getUserModernLanguages(this.userGeneralData.id);
      }
      else {
        this.addOrEditLanguagesButton = false;
        this.isLoadingLanguages = false;
        this.userModernLanguages = null;
      }

      if (userData.recomendationsForStudyCount != 0) {

          this.getUserRecommendation(this.userGeneralData.id);
        }
        else {
          this.addOrEditRecommendationsButton = false;
          this.isLoadingRecommandations = false;
          this.recommendationForStudy = null;
        }
    })
  }

  getUserStudies(userId) {
    this.isLoadingStudies = true;

    const request = {
      userProfileId: userId
    }
    this.studyService.list(request).subscribe(res => {
      this.userStudies = res.data.items;
      this.initExistentStudyForm(this.userStudies);
      this.isLoadingStudies = false;
      this.addOrEditStudyButton = true;
    })
  }

  getUserModernLanguages(userId) {
    this.isLoadingLanguages = true;

    const request = {
      userProfileId: userId
    }
    this.modernLanguageLevelService.list(request).subscribe(res => {
      this.userModernLanguages = res.data.items;
      this.initExistentLanguageForm(this.userModernLanguages);
      this.isLoadingLanguages = false;
      this.addOrEditLanguagesButton = true;
    })
  }

  getUserRecommendation(userId) {
    this.isLoadingRecommandations = true;

    const request = {
      userProfileId: userId
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

  generateStudies(study?, userProfileId?): FormGroup {

    return this.fb.group({
      id: this.fb.control((study && study.id) || null, []),
      institution: this.fb.control((study && study.institution) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      institutionAddress: this.fb.control((study && study.institutionAddress) ||  null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      studyTypeId: this.fb.control((study && study.studyTypeId) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyFrequency: this.fb.control((study && study.studyFrequency) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      faculty: this.fb.control((study && study.faculty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      specialty: this.fb.control((study && study.specialty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      yearOfAdmission: this.fb.control((study && study.yearOfAdmission) || null, [Validators.required]),
      graduationYear: this.fb.control((study && study.graduationYear) || null, [Validators.required]),
      userProfileId: this.fb.control(userProfileId || null, [])
    });
  }

  generateModernLanguage(language?, modernLanguageId?, userProfileId?) {
    
    return this.fb.group({
      id: this.fb.control((language && language.id) || null, []),
      knowledgeQuelifiers: this.fb.control((language && language.knowledgeQuelifiers)  || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      userProfileId: this.fb.control(userProfileId || null, []),
      modernLanguageId: this.fb.control((modernLanguageId && modernLanguageId.id)  || null, []),
    });
  }

  generateRecommendation(recommendation?, userProfileId?) {
    
    return this.fb.group({
      id: this.fb.control((recommendation && recommendation.id) || null, []),
      name: this.fb.control((recommendation && recommendation.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((recommendation && recommendation.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((recommendation && recommendation.function) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      subdivision: this.fb.control((recommendation && recommendation.subdivision) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      userProfileId: this.fb.control(userProfileId || null, []),
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
      this.notificationService.success('Success', 'Studies added!', NotificationUtil.getDefaultMidConfig());
      this.getUserStudies(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Studies was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  creteModernLanguage(){
    this.buildModernLanguageForm().subscribe(response => {
      this.notificationService.success('Success', 'Modern Language added!', NotificationUtil.getDefaultMidConfig());
      this.getUserModernLanguages(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Modern Language was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  creteRecommendationForStudy(){
    this.buildReommendationForStudyForm().subscribe(response => {
      this.notificationService.success('Success', 'Recommendation added!', NotificationUtil.getDefaultMidConfig());
      this.getUserRecommendation(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Recommendation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  buildStudiesForm(): Observable<any> {
    const request = this.parseStudies(this.studyForm.getRawValue().studies, this.userId);
    return this.studyService.addMultiple(request);
  }

  buildModernLanguageForm(): Observable<any> {
    const request = this.parseModernLanguages(this.languageForm.getRawValue().modernLanguages, this.userId);
    return this.modernLanguageLevelService.addMultiple(request);
  }

  buildReommendationForStudyForm(): Observable<any> {
    const request = this.parseRecommendationsForStudy(this.recommendationForm.getRawValue().recommendation, this.userId);
    return this.recommendationForStudyService.addMultiple(request);
  }

  parseStudies(data: StudyModel[], userProfileId: number): StudyModel[] {
    return data.map(el => this.parseStudy(el, userProfileId));
  }

  parseModernLanguages(data: ModernLanguageLevelModel[], userProfileId: number): ModernLanguageLevelModel[] {
    return data.map(el => this.parseModernLanguage(el, userProfileId));
  }

  parseRecommendationsForStudy(data: RecommendationForStudyModel[], userProfileId: number): RecommendationForStudyModel[] {
    return data.map(el => this.parseRecomendationForStudy(el, userProfileId));
  }

  parseStudy(data: StudyModel, userProfileId): StudyModel {
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
      userProfileId: userProfileId
    })
  }

  parseModernLanguage(data: ModernLanguageLevelModel, userProfileId): ModernLanguageLevelModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      modernLanguageId: data.modernLanguageId,
      knowledgeQuelifiers: data.knowledgeQuelifiers,
      userProfileId: userProfileId
    })
  }

  parseRecomendationForStudy(data: RecommendationForStudyModel, userProfileId): RecommendationForStudyModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      function: data.function,
      subdivision: data.subdivision,
      userProfileId: userProfileId
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
          this.userId
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
          language.modernLanguage,
          this.userId
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
          this.userId
        )
      );
    }
  }

  getExistentStep(step){
    const request = {
      userProfileId : this.userId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  onAddTitle() {
    this.isDoneStep.emit({ isDone: this.isDone });
  }

  addRegistrationFluxStep(){
    if(this.userStudies != null || this.userModernLanguages != null || this.recommendationForStudy != null){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.userId);
    
    }
    else{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.userId);
    }
  }

  checkRegistrationStep(stepData, stepId, success, userId){
    const datas= {
      isDone: success,
      stepId: this.stepId
    }

    if(stepData.length == 0){
      this.addCandidateRegistationStep(success, stepId, userId);
      this.ds.sendData(datas);
    }else{
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, userId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, userId ){
    const request = {
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, userId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    
    this.registrationFluxService.update(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
}
