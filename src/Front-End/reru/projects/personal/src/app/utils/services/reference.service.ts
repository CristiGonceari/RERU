import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { SelectItem } from '../models/select-item.model';

@Injectable({
  providedIn: 'root'
})
export class ReferenceService extends AbstractService {
  private readonly urlRoute = 'Reference';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  listNomenclatureTypes(): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/nomenclature-types/select-values`);
  }

  listNomenclatureType(id: number): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/nomenclature/${id}/select-values`);
  }

  listContractorTypes(): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/contractor-types/select-values`);
  }

  listDepartments(): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/departments/select-values`);
  }

  listOrganizationRoles(): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/organization-roles/select-values`);
  }

  listContactTypes(): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/contact-types/select-values`);
  }

  listChartOrganizationRoles(id: number): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/organization-roles/chart/${id}`);
  }

  listChartDepartments(id: number): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/departments/chart/${id}`);
  }

  listNomenclatureRecords(data): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/nomenclature-records/select-values`, {params: data });
  }

  listContractors(contractorId: number): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/contractors/select-values/${contractorId}`);
  }

  listAllContractors(): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/contractors/select-values`);
  }

  listTimeSheetValues(): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/time-sheet/select-values`);
  }

  listContractorEmails(contractorId: number): Observable<ApiResponse<SelectItem[]>> {
    return this.http.get<ApiResponse<SelectItem[]>>(`${this.baseUrl}/${this.urlRoute}/emails/${contractorId}`);
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

  getArticleRoles(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/article-roles/select-values`);
  }
}
