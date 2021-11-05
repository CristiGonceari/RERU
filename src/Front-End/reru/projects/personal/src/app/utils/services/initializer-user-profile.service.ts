import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { UserProfileModel } from '../models/user-profile.model';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InitializerUserProfileService extends AbstractService {
  private readonly routeUrl = 'UserProfile';
  profile: BehaviorSubject<UserProfileModel> = new BehaviorSubject<UserProfileModel>(null);
  constructor(appConfigService: AppSettingsService, private http: HttpClient) {
    super(appConfigService);
   }

  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${this.routeUrl}`).pipe(
      switchMap(response => {
        this.profile.next(response.data);
        return of(response);
      })
    )
  }
}
