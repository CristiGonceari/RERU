import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Exception404Component } from './exceptions/404/404.component';
import { Exception500Component } from './exceptions/500/500.component';
import { NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateComponent } from './components/date/date.component';
import { ConfirmModalComponent } from './modals/confirm-modal/confirm-modal.component';
import { TranslateModule } from '@ngx-translate/core';
import { PaginationComponent } from './components/pagination/pagination.component';
import { NgbDateFRParserFormatter } from './services/date-parse-formatter.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DeleteDepartmentModalComponent } from './modals/delete-department-modal/delete-department-modal.component';
import { ChangeNameModalComponent } from './modals/change-name-modal/change-name-modal.component';
import { ChangeCurrentPositionModalComponent } from './modals/change-current-position-modal/change-current-position-modal.component';
import { AddContractorContactModalComponent } from './modals/add-contractor-contact-modal/add-contractor-contact-modal.component';
import { EditContactModalComponent } from './modals/edit-contact-modal/edit-contact-modal.component';
import { ConfirmationDismissModalComponent } from './modals/confirmation-dismiss-modal/confirmation-dismiss-modal.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { ConfirmationContactDeleteModalComponent } from './modals/confirmation-contact-delete-modal/confirmation-contact-delete-modal.component';
import { ConfirmationDeleteContractorComponent } from './modals/confirmation-delete-contractor/confirmation-delete-contractor.component';
import { PersonalDropdownDetailsComponent } from './components/personal-dropdown-details/personal-dropdown-details.component';
import { RouterModule } from '@angular/router';
import { ViewContactComponent } from './components/view-contact/view-contact.component';
import { ViewContactDropdownComponent } from './components/view-contact-dropdown/view-contact-dropdown.component';
import { TransferNewPositionModalComponent } from './modals/transfer-new-position-modal/transfer-new-position-modal.component';
import { AddOldPositionModalComponent } from './modals/add-old-position-modal/add-old-position-modal.component';
import { OrganigramActionModalComponent } from './modals/organigram-action-modal/organigram-action-modal.component';
import { DeleteOrganigramModalComponent } from './modals/delete-organigram-modal/delete-organigram-modal.component';
import { AddAccessModalComponent } from './modals/add-access-modal/add-access-modal.component';
import { DeleteVacationModalComponent } from './modals/delete-vacation-modal/delete-vacation-modal.component';
import { DeleteNomenclatureModalComponent } from './modals/delete-nomenclature-modal/delete-nomenclature-modal.component';
import { ConfirmResetPasswordModalComponent } from './modals/confirm-reset-password-modal/confirm-reset-password-modal.component';
import { LastOrganizationRoleLabelComponent } from './components/last-organization-role-label/last-organization-role-label.component';
import { LastDepartmentLabelComponent } from './components/last-department-label/last-department-label.component';
import { DepartmentContentModalComponent } from './modals/department-content-modal/department-content-modal.component';
import { DeleteDepartmentContentModalComponent } from './modals/delete-department-content-modal/delete-department-content-modal.component';
import { AddHolidayModalComponent } from './modals/add-holiday-modal/add-holiday-modal.component';
import { DeleteHolidayModalComponent } from './modals/delete-holiday-modal/delete-holiday-modal.component';
import { EditHolidayModalComponent } from './modals/edit-holiday-modal/edit-holiday-modal.component';
import { DeleteContractorTypeModalComponent } from './modals/delete-contractor-type-modal/delete-contractor-type-modal.component';
import { ContentComponent } from './components/content/content.component';
import { DisableNomenclatureModalComponent } from './modals/disable-nomenclature-modal/disable-nomenclature-modal.component';
import { AddNomenclatureHeaderModalComponent } from './modals/add-nomenclature-header-modal/add-nomenclature-header-modal.component';
import { AddNomenclatureRecordModalComponent } from './modals/add-nomenclature-record-modal/add-nomenclature-record-modal.component';
import { AddDocumentModalComponent } from './modals/add-document-modal/add-document-modal.component';
import { AddPhotoModalComponent } from './modals/add-photo-modal/add-photo-modal.component';
import { FieldGeneratorComponent } from './components/field-generator/field-generator.component';
import { ConfirmationDeleteDocumentComponent } from './modals/confirmation-delete-document/confirmation-delete-document.component';
import { RecordTextGeneratorComponent } from './components/record-text-generator/record-text-generator.component';
import { DeleteColumnModalComponent } from './modals/delete-column-modal/delete-column-modal.component';
import { BulletinAddressModalComponent } from './modals/bulletin-address-modal/bulletin-address-modal.component';
import { EditStydyModalComponent } from './modals/edit-stydy-modal/edit-stydy-modal.component';
import { DeleteStudyModalComponent } from './modals/delete-study-modal/delete-study-modal.component';
import { AskGenerateOrderModalComponent } from './modals/ask-generate-order-modal/ask-generate-order-modal.component';
import { ImportRoleModalComponent } from './modals/import-role-modal/import-role-modal.component';
import { DeleteInstructionModalComponent } from './modals/delete-instruction-modal/delete-instruction-modal.component';
import { ViewDatePipe } from './pipes/view-date.pipe';
import { AddInstructionModalComponent } from './modals/add-instruction-modal/add-instruction-modal.component';
import { BadgeStateComponent } from './components/badge-state/badge-state.component';
import { AvatarDetailsComponent } from './components/avatar-details/avatar-details.component';
import { AddVacationModalComponent } from './modals/add-vacation-modal/add-vacation-modal.component';
import { VacationStateComponent } from './components/vacation-state/vacation-state.component';
import { AddRequestModalComponent } from './modals/add-request-modal/add-request-modal.component';
import { AddRankModalComponent } from './modals/add-rank-modal/add-rank-modal.component';
import { EditRankModalComponent } from './modals/edit-rank-modal/edit-rank-modal.component';
import { DeleteRankModalComponent } from './modals/delete-rank-modal/delete-rank-modal.component';
import { AddFamilyModalComponent } from './modals/add-family-modal/add-family-modal.component';
import { DeleteFamilyModalComponent } from './modals/delete-family-modal/delete-family-modal.component';
import { EditFamilyModalComponent } from './modals/edit-family-modal/edit-family-modal.component';
import { SignFileModalComponent } from './modals/sign-file-modal/sign-file-modal.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SignDirective } from './directives/sign.directive';
import { AddRequestByHrModalComponent } from './modals/add-request-by-hr-modal/add-request-by-hr-modal.component';
import { AddEditTimesheetValuesComponent } from './modals/add-edit-timesheet-values/add-edit-timesheet-values.component';
import { DeleteDocumentsTemplatesModalComponent } from './modals/delete-documents-templates-modal/delete-documents-templates-modal.component';
import { ConvertPdfDocumentModalComponent } from './modals/convert-pdf-document-modal/convert-pdf-document-modal.component';
import { CkEditorConfigComponent } from './components/ck-editor-config/ck-editor-config.component'
import { CKEditorModule } from 'ngx-ckeditor';
import { MaterialModule } from '../material.module';
import { SharedModule } from '@erp/shared';
import { ImportDepartmentsModalComponent } from './modals/import-departments-modal/import-departments-modal.component';

const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  ConfirmModalComponent,
  PaginationComponent,
  DeleteDepartmentModalComponent,
  ChangeNameModalComponent,
  ChangeCurrentPositionModalComponent,
  AddContractorContactModalComponent,
  EditContactModalComponent,
  ConfirmationDismissModalComponent,
  SafeHtmlPipe,
  ConfirmationContactDeleteModalComponent,
  ConfirmationDeleteContractorComponent,
  PersonalDropdownDetailsComponent,
  ViewContactComponent,
  ViewContactDropdownComponent,
  TransferNewPositionModalComponent,
  AddOldPositionModalComponent,
  OrganigramActionModalComponent,
  DeleteOrganigramModalComponent,
  AddAccessModalComponent,
  DeleteVacationModalComponent,
  DeleteNomenclatureModalComponent,
  ConfirmResetPasswordModalComponent,
  LastOrganizationRoleLabelComponent,
  LastDepartmentLabelComponent,
  DepartmentContentModalComponent,
  DeleteDepartmentContentModalComponent,
  AddHolidayModalComponent,
  DeleteHolidayModalComponent,
  EditHolidayModalComponent,
  DeleteContractorTypeModalComponent,
  ContentComponent,
  DisableNomenclatureModalComponent,
  AddNomenclatureHeaderModalComponent,
  AddNomenclatureRecordModalComponent,
  AddDocumentModalComponent,
  AddPhotoModalComponent,
  FieldGeneratorComponent,
  ConfirmationDeleteDocumentComponent,
  RecordTextGeneratorComponent,
  DeleteColumnModalComponent,
  BulletinAddressModalComponent,
  EditStydyModalComponent,
  DeleteStudyModalComponent,
  AskGenerateOrderModalComponent,
  ImportRoleModalComponent,
  DeleteInstructionModalComponent,
  ViewDatePipe,
  AddInstructionModalComponent,
  BadgeStateComponent,
  AvatarDetailsComponent,
  AddVacationModalComponent,
  VacationStateComponent,
  AddRequestModalComponent,
  AddRankModalComponent,
  EditRankModalComponent,
  DeleteRankModalComponent,
  AddFamilyModalComponent,
  DeleteFamilyModalComponent,
  EditFamilyModalComponent,
  SignFileModalComponent,
  SignDirective,
  AddRequestByHrModalComponent,
  AddEditTimesheetValuesComponent,
  DeleteDocumentsTemplatesModalComponent,
  ConvertPdfDocumentModalComponent,
  CkEditorConfigComponent,
  ImportDepartmentsModalComponent

];

@NgModule({
  declarations: commonComponents,
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    CKEditorModule,
    MaterialModule,
    SharedModule
  ],
  exports: [
    TranslateModule,
    commonComponents
  ],
  providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter }, SafeHtmlPipe, ViewDatePipe],
  entryComponents: [
    ConfirmModalComponent,
    DeleteDepartmentModalComponent,
    ChangeNameModalComponent,
    ChangeCurrentPositionModalComponent,
    AddContractorContactModalComponent,
    EditContactModalComponent,
    ConfirmationDismissModalComponent,
    ConfirmationContactDeleteModalComponent,
    ConfirmationDeleteContractorComponent,
    TransferNewPositionModalComponent,
    AddOldPositionModalComponent,
    OrganigramActionModalComponent,
    DeleteOrganigramModalComponent,
    AddAccessModalComponent,
    DeleteVacationModalComponent,
    DeleteNomenclatureModalComponent,
    ConfirmResetPasswordModalComponent,
    DepartmentContentModalComponent,
    DeleteDepartmentContentModalComponent,
    AddHolidayModalComponent,
    DeleteHolidayModalComponent,
    EditHolidayModalComponent,
    DeleteContractorTypeModalComponent,
    DisableNomenclatureModalComponent,
    AddNomenclatureHeaderModalComponent,
    AddNomenclatureRecordModalComponent,
    AddDocumentModalComponent,
    ConfirmationDeleteDocumentComponent,
    DeleteColumnModalComponent,
    BulletinAddressModalComponent,
    EditStydyModalComponent,
    DeleteStudyModalComponent,
    AskGenerateOrderModalComponent,
    ImportRoleModalComponent,
    DeleteInstructionModalComponent,
    AddInstructionModalComponent,
    AddVacationModalComponent,
    AddRequestModalComponent,
    AddRankModalComponent,
    EditRankModalComponent,
    DeleteRankModalComponent,
    AddFamilyModalComponent,
    DeleteFamilyModalComponent,
    EditFamilyModalComponent,
    SignFileModalComponent,
    AddRequestByHrModalComponent,
    ConvertPdfDocumentModalComponent,
    CkEditorConfigComponent,
    ImportDepartmentsModalComponent

  ],
})
export class UtilsModule { }
