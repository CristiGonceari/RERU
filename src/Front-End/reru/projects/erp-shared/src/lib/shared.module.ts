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
import { AddEditMediaFileComponent } from './components/add-edit-media-file/add-edit-media-file.component';
import { UploadFileModalComponent } from './modals/upload-file-modal/upload-file-modal.component';
import { INTERNAL_NOTIFY_INTERCEPTOR } from './interceptors/internal-notify.interceptor';
import { GoToTestModalComponent } from './modals/go-to-test-modal/go-to-test-modal.component';
import { LayoutContentComponent } from './components/layout-content/layout-content.component';
import { ContentComponent } from './components/content/content.component';
import { Exception404Component } from './components/404/404.component';
import { Exception500Component } from './components/500/500.component';
import { FormsModule } from '@angular/forms';
import { ImportButtonComponent } from './components/buttons/import-button/import-button.component';
import { ExportButtonComponent } from './components/buttons/export-button/export-button.component';

import { OverlayModule } from '@angular/cdk/overlay';
import { CdkTreeModule } from '@angular/cdk/tree';
import { PortalModule } from '@angular/cdk/portal';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { MatBadgeModule } from '@angular/material/badge';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatTooltipModule } from '@angular/material/tooltip';

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
	ShowImageModalComponent,
	ContentComponent,
	Exception404Component,
	Exception500Component,
	ExportButtonComponent,
	ImportButtonComponent,
];

const materialModules = [
	CdkTreeModule,
	MatAutocompleteModule,
	MatButtonModule,
	MatCardModule,
	MatCheckboxModule,
	MatChipsModule,
	MatDividerModule,
	MatExpansionModule,
	MatIconModule,
	MatInputModule,
	MatListModule,
	MatMenuModule,
	MatProgressSpinnerModule,
	MatPaginatorModule,
	MatRippleModule,
	MatSelectModule,
	MatSidenavModule,
	MatSnackBarModule,
	MatSortModule,
	MatTableModule,
	MatTabsModule,
	MatToolbarModule,
	MatFormFieldModule,
	MatButtonToggleModule,
	MatTreeModule,
	OverlayModule,
	PortalModule,
	MatBadgeModule,
	MatGridListModule,
	MatRadioModule,
	MatDatepickerModule,
	MatTooltipModule
  ];
@NgModule({
	declarations: [
		...commonExports,
		ViewIconComponent,
		LayoutContentComponent,
		UploadFileModalComponent,
		GoToTestModalComponent
	],
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
		FormsModule,
		materialModules
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

