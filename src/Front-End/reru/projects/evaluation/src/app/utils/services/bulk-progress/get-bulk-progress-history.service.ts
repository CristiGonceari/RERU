import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetBulkProgressHistoryService extends AbstractService {

  private readonly urlRoute = 'Process';
  private sendToCancelRequest = new BehaviorSubject('Basic Approval is required!');
  currentSendToCancelRequest = this.sendToCancelRequest.asObservable();
  
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getProgressHistory(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`);
  }

  closeAllProcesses(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/close`);
  }

  cancelRequest(data) {
    this.sendToCancelRequest.next(data)
  }
}
