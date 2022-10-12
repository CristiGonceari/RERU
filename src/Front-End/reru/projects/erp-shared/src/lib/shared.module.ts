import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderMobileComponent } from './components/header-mobile/header-mobile.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SidebarRightComponent } from './components/sidebar-right/sidebar-right.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { PageTitleComponent } from './components/page-title/page-title.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { ViewIconComponent } from './components/sidenav/components/view-icon/view-icon.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AUTHENTICATION_HEADER_INTERCEPTOR_PROVIDER } from './interceptors/authentication-header.interceptor';
import { HTTP_ERROR_INTERCEPTOR_PROVIDER } from './interceptors/http-error.interceptor';
import { UNAUTHORIZED_INTERCEPTOR_PROVIDER } from './interceptors/unauthorized.interceptor';
import { ContentComponent } from './components/content/content.component';
import { HttpClientModule } from '@angular/common/http';

import { LocalizeRouterModule } from '@gilsdav/ngx-translate-router';
import { AddButtonComponent } from './components/buttons/add-button/add-button.component';
import { ConfirmButtonComponent } from './components/buttons/confirm-button/confirm-button.component';
import { DeleteButtonComponent } from './components/buttons/delete-button/delete-button.component';
import { ButtonComponent } from './components/buttons/button/button.component';
import { PermissionDirective } from './directives/permission.directive';
import { HTTP_RESPONSE_INTERCEPTOR_PROVIDER } from './interceptors/response.interceptor';
import { PermissionRouteGuard } from './guards/permission-route.guard';
import { SharedPipesModule } from './modules/shared-pipes.module';
import { SvgModule } from './modules/svg.module';
import { DEACTIVATED_INTERCEPTOR_PROVIDER } from './interceptors/deactivated.interceptor';
import { AppSettingsService } from './services/app-settings.service';
//import { MODULE_CONFIG_INITIALIZER } from './initializers/module.initializer';
//import { appInitializerFn, initAuthenticationServiceFn, loadApplicationUserFn } from './factories/initializer.factory';
import { appInitializerFn } from './factories/initializer.factory';
import { ApplicationUserService } from './services/application-user.service';
import { AuthenticationService } from './services/authentication.service';
import { AuthenticationCallbackComponent } from './components/authentication/authentication-callback/authentication-callback.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { PrintModalComponent } from './modals/print-modal/print-modal.component';
import { LocationBackButtonComponent } from './components/location-back-button/location-back-button.component';
import { SearchInputComponent } from './components/search-input/search-input.component';
import { GetMediaFileComponent } from './components/get-media-file/get-media-file.component';
import { ShowImageModalComponent } from './modals/show-image-modal/show-image-modal.component';
import { AddEditMediaFileComponent } from '../public-api';
import { UploadFileModalComponent } from './modals/upload-file-modal/upload-file-modal.component';
import { INTERNAL_NOTIFY_INTERCEPTOR } from './interceptors/internal-notify.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';

export const translateModule = TranslateModule.forChild();

const commonExports = [
	HeaderMobileComponent,
	HeaderComponent,
	SidebarComponent,
	SidebarRightComponent,
	FooterComponent,
	PageTitleComponent,
	SidenavComponent,
	LayoutComponent,
	AddButtonComponent,
	ConfirmButtonComponent,
	DeleteButtonComponent,
	ButtonComponent,
	PermissionDirective,
	AuthenticationCallbackComponent,
	PaginationComponent,
	LoadingSpinnerComponent,
	PrintModalComponent,
	LocationBackButtonComponent,
	SearchInputComponent,
	AddEditMediaFileComponent,
	GetMediaFileComponent,
	ShowImageModalComponent
];
@NgModule({
	declarations: [...commonExports, ViewIconComponent, ContentComponent, UploadFileModalComponent],
	imports: [
		CommonModule,
		RouterModule,
		HttpClientModule,
		translateModule,
		NgbModule,
		LocalizeRouterModule,
		SharedPipesModule,
		SvgModule,
		NgxDropzoneModule,
		NgxSpinnerModule
	],
	exports: [...commonExports],
	providers: [
		{
			provide: APP_INITIALIZER,
			useFactory: appInitializerFn,
			multi: true,
			deps: [AppSettingsService, AuthenticationService, ApplicationUserService],
		},
   // DEPENDENTED_SERVICE_PROVIDER
	//	,
		//	TranslatePipe,
		// ApplicationUserService,
		// AppSettingsService,
		PermissionRouteGuard,
		AUTHENTICATION_HEADER_INTERCEPTOR_PROVIDER,
		UNAUTHORIZED_INTERCEPTOR_PROVIDER,
		HTTP_ERROR_INTERCEPTOR_PROVIDER,
		DEACTIVATED_INTERCEPTOR_PROVIDER,
		HTTP_RESPONSE_INTERCEPTOR_PROVIDER,
		INTERNAL_NOTIFY_INTERCEPTOR
	],
})
export class SharedModule {
	public static forRoot(environment: any): ModuleWithProviders<SharedModule> {

        return {
            ngModule: SharedModule,
            providers: [
                {
                    provide: 'env',
                    useValue: environment
                }
            ]
        };
    }
}

