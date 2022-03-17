import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FaqListComponent } from './faq-list/faq-list.component';
import { FaqAddEditComponent } from './faq-add-edit/faq-add-edit.component';
import { FaqListTableComponent } from './faq-list/faq-list-table/faq-list-table.component';
import { FaqRoutingModule } from './faq-routing.module';
import { SearchComponent } from './faq-list/search/search.component';
import { UtilsModule } from '../../utils/utils.module';
import { FaqDropdownDetailsComponent } from './faq-list/faq-dropdown-details/faq-dropdown-details.component';
import { DetailsComponent } from './details/details.component';

@NgModule({
	declarations: [
		FaqListComponent,
		FaqAddEditComponent,
		FaqListTableComponent,
		SearchComponent,
		FaqDropdownDetailsComponent,
		DetailsComponent,
	],
	imports: [
	  CommonModule,
		ReactiveFormsModule,
		FormsModule,
		NgbModule,
		TranslateModule,
		SharedModule,
		CKEditorModule,
		FaqRoutingModule,
		UtilsModule
	],
	exports: [],
	entryComponents: [],
	providers: [NgbActiveModal],
})
export class FAQModule {}
