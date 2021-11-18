// import { Injectable } from '@angular/core';
// import { HttpClient, HttpBackend } from '@angular/common/http';
// import { IAppConfig } from '../models/app-config.model';
// import { environment } from '../../../environments/environment';

// @Injectable({
//   providedIn: 'root'
// })
// export class AppConfigService {
//   private readonly http: HttpClient;
//   private readonly path = 'assets/config/appsettings.json';
//   private appConfig: IAppConfig = new IAppConfig();
//   constructor(handler: HttpBackend) {
//     this.http = new HttpClient(handler);
//   }

//   load(): Promise<IAppConfig> {
//     return this.http.get(this.path)
//                     .toPromise()
//                     .then((config: IAppConfig) => this.appConfig = config)
//                     .catch(error => {
//                       console.error('Error loading app-config.json, using environment file instead');
//                       console.error(error);
//                       this.appConfig = environment;

//                       return Promise.resolve(this.appConfig);
//                     });
//   }

//   get config(): IAppConfig {
//     return this.appConfig;
//   }
// }
