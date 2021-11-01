import { ApplicationModule } from '@angular/core';
import { ApplicationModuleModel } from './application-module.model';

export class ApplicationUserModuleModel {
	role: string;
	permissions: string[];
	module: ApplicationModuleModel;
}
