import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root',
})
export class InternalService extends AbstractService {
  private readonly routeUrl: string = 'Application';

  constructor(
    protected configService: AppSettingsService,
    private http: HttpClient
  ) {
    super(configService);
  }

  getTestIdForFastStart(): Observable<any> {
    return this.http.get<any>(`http://localhost:7001/internal/api/Application`);
  }
  
}
