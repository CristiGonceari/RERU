import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { UserForRemove } from '../models/user-for-remove.model';
import { EditUserPersonalDetails } from '../models/edit-user-personal-details.model';
import { PersonalData } from '../models/personal-data.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class UserService extends AbstractService {
	private readonly routeUrl: string = 'User';

	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	getUserForRemove(id: number): Observable<Response<UserForRemove>> {
		return this.http.get<Response<UserForRemove>>(`${this.coreUrl}/${this.routeUrl}/${id}/for-remove`);
	}

	createUser(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}`, data);
	}

	addUserAvatar(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}/avatar`, data);
	}

	getUser(id: string): Observable<Response<User>> {
		return this.http.get<Response<User>>(`${this.coreUrl}/${this.routeUrl}/${id}`);
	}

	getEditUserPersonalDetails(id: number): Observable<Response<EditUserPersonalDetails>> {
		return this.http.get<Response<EditUserPersonalDetails>>(
			`${this.coreUrl}/${this.routeUrl}/${id}/personal-details/edit`
		);
	}

	getPersonalData(): Observable<Response<PersonalData>> {
		return this.http.get<Response<PersonalData>>(`${this.coreUrl}/${this.routeUrl}/get-personal-data`);
	}

	bulkAddUsers(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/excel-import`, data);
	}

	editUser(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}`, data);
	}

	editUserPersonalDetails(data): Observable<any> {
		return this.http.patch(`${this.coreUrl}/${this.routeUrl}/personal-details`, {data});
	}

	resetPassword(id: string): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/${id}/password-reset`, {});
	}

	removeUser(id: number): Observable<any> {
		return this.http.delete(`${this.coreUrl}/${this.routeUrl}/${id}/remove-user`, {});
	}

	blockUnblockUser(id: string): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/${id}/block-unblock-user`, {});
	}

	activateUser(id: string): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/${id}/activate`, {});
	}

	deactivateUser(id: string): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/${id}/deactivate`, {});
	}

	changePersonalData(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/change-my-data`, data);
	}

	changePassword(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/change-my-password`, data);
	}

	setPassword(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/set-password`, data);
	}
}
