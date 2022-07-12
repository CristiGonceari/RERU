import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReferenceService extends AbstractService{

  private readonly urlRoute = 'Reference';

  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getProcesses(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/processes-value/select-values`);
  }

  getCandidateSexEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/candidate-sex/select-values`);
  }

  getCandidateStateLanguageLevelEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/candidate-state-language-level/select-values`);
  }

  getCandidateNationalities(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/candidate-nationalities/select-values`);
  }

  getCandidateCitizenship(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/candidate-citizens/select-values`);
  }
  
  getRegistrationFluxStepsEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/registration-flux-steps/select-values`);
  }

  getStudyFrequencyEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/studies-frequency/select-values`);
  }

  getStudyTypes(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/study-types/select-values`);
  }

  getModernLanguages(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/modern-languages/select-values`);
  }

  getKnowledgeQuelifiersEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/knowledge-quelifiers/select-values`);
  }

  getMaterialStatusType(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/material-status-type/select-values`);
  }

  getKinshipDegreeEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/kinship-degree/select-values`);
  }

  getMilitaryObligationTypeEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/military-obligation-type-enum/select-values`);
  }

}
