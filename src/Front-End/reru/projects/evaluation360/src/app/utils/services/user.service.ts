import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class UserService extends AbstractService {
  private readonly routeUrl: string = 'user';
  constructor(protected configService: AppSettingsService, 
              private http: HttpClient, 
              private router: Router) {
    super(configService);
   }

   getForEdit(id: number): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}/edit`);
   }

   updateUser(data): Observable<any> {
     return this.http.put(`${this.baseUrl}/${this.routeUrl}`, data);
   }

   delete(id: number) {
     return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
   }

   setPassword(id: number, data): Observable<any> {
      return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}/password`, data);
   }

   changePassword(id: number, data): Observable<any> {
      return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}/change-password`, data); 
   }

   resetPassword(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/email-verification`, { params: data });
   }

   confirmPassword(data): Observable<any> {
     return this.http.post(`${this.baseUrl}/${this.routeUrl}/password-reset`, data);
   }

   login(data): Observable<any> {
     return this.http.post(`${this.baseUrl}/${this.routeUrl}/sign-in`, data);
   }

   add(data): Observable<any> {
     return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
   }

   list(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
   }

   sendMassMail(): Observable<any> {
     return this.http.put(`${this.baseUrl}/${this.routeUrl}/mass-email`, {});
   }

   getUser() {
     const user = localStorage.getItem('evaluation360');
     if (!user) {
       return;
     }

     return JSON.parse(user);
   }

   logout(): void {
    localStorage.removeItem('evaluation360');
    localStorage.removeItem('access_token');
    this.router.navigate(['/login']);
   }
}
