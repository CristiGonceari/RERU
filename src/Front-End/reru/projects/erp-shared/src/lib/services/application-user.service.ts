import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthResponse } from '../models/auth-response.model';
import { ApplicationUserModel } from '../models/application-user.model';
import { Response } from '../models/response';
import { AbstractService } from './abstract.service';
import { AppSettingsService } from './app-settings.service';

@Injectable(
  {
    providedIn: "root"
  }
)
export class ApplicationUserService extends AbstractService {
	private readonly userProfileKey = 'user';
	private tokenSubject: BehaviorSubject<AuthResponse> = new BehaviorSubject<AuthResponse>(null);

	private userSubject: BehaviorSubject<ApplicationUserModel> = new BehaviorSubject<ApplicationUserModel>(null);
	public userChange = this.userSubject.asObservable();
	constructor(
    private appSettingsService: AppSettingsService,
    private http: HttpClient) {
    super(appSettingsService);
   console.log('==> application user service constructor');
  }

	loadCurrentUser(data?: AuthResponse): Promise<boolean> {
		if (data) {
			this.tokenSubject.next(data);
		}

		return this.http
			.get<Response<ApplicationUserModel>>(`${this.coreUrl}/me`, {
				withCredentials: true,
				headers: this.setCurrentHeaders(data && data.access_token),
			})
			.toPromise()
			.then(
				(response: Response<ApplicationUserModel>) => {
					console.log('==> application user service /me response', response);
					if (response.data && response.data.isAuthenticated) {
						localStorage.setItem(this.userProfileKey, JSON.stringify(response.data));
						this.userSubject.next(response.data);

						return true;
					} else {
						this.sessionLogout(response.data);
						return false;
					}
				},
				error => {
					this.sessionLogout(null);
					return false;
				}
			);
	}

	setCurrentHeaders(token: string): HttpHeaders {
		if (!token) {
			return null;
		}

		return new HttpHeaders({
			Authorization: `Bearer ${token}`,
		});
	}

	setCurrentUser(user): void {
		localStorage.setItem(this.userProfileKey, JSON.stringify(user));
		this.userSubject.next(user);
	}

	getCurrentUser(): ApplicationUserModel {
		return this.userSubject.value;
	}

	getCurrentToken(): AuthResponse {
		return this.tokenSubject.value;
	}

	logoutCurrentUser(): void {
		localStorage.removeItem(this.userProfileKey);
	}

	sessionLogout(user): void {
		this.logoutCurrentUser();
		this.userSubject.next(user);
	}
}
