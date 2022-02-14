import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ObjectUtil } from '../../../utils/util/object.util';
import { saveAs } from 'file-saver';
import { FileService } from '../../../utils/services/file.service';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';
import { Contractor } from '../../../utils/models/contractor.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { PermissionModel } from '../../../utils/models/permission.model';
import { DocumentTypeEnum } from '../../../utils/models/document-type.enum';
import { InitializerUserProfileService } from '../../../utils/services/initializer-user-profile.service';
import { UserProfileModel } from '../../../utils/models/user-profile.model';

@Component({
	selector: 'app-profile-documents',
	templateUrl: './profile-documents.component.html',
	styleUrls: ['./profile-documents.component.scss'],
})
export class ProfileDocumentsComponent implements OnInit {
  @Input() contractor: Contractor;
  @Input() permissions: PermissionModel;
  @Output() update: EventEmitter<void> = new EventEmitter<void>();
  documents: any[] = [];
  pagedSummary: PagedSummary = {
    currentPage: 1,
    totalCount: 0,
    totalPages: 0,
    pageSize: 10
  }
  isLoading: boolean = true;
  selectedType: number = 4;
  isNotFound: boolean;
  contractorId: number;
  constructor(private fileService: FileService,
              private contractorProfileService: ContractorProfileService,
              private initializerProfileService: InitializerUserProfileService) { }

  ngOnInit(): void {
    if (!this.contractor) {
      const profile: UserProfileModel = this.initializerProfileService.profile.getValue();
      this.contractorId = profile.contractorId;
    }

		if (this.hasAllDocumentAccess()) {
			this.retrieveDocuments();
		}
	}

	hasAllDocumentAccess(): boolean {
		if (this.permissions.getDocumentsDataOrders && this.selectedType === 4) {
			return true;
		}

		this.isNotFound = true;
		return false;
	}

  retrieveDocuments(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      contractorId: this.contractor && +this.contractor.id || this.contractorId,
      type: this.selectedType == 0 ? null : this.selectedType || 4,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage
    });
    this.setNotFound();
    this.contractorProfileService.getFiles(request).subscribe(response => {
      this.documents = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    })
  }

  setNotFound(): void {
    if (!this.permissions.getDocumentsDataOrders && this.selectedType ==  DocumentTypeEnum.Order ||
      !this.permissions.getDocumentsDataIdentity && this.selectedType ==  DocumentTypeEnum.Identity ||
      !this.permissions.getDocumentsDataCim && this.selectedType ==  DocumentTypeEnum.IEC ||
      !this.permissions.getDocumentsDataRequest && this.selectedType ==  DocumentTypeEnum.Request ) {
      this.isNotFound = true;
      this.isLoading = false;
    } else {
      this.isNotFound = false;
    }
  }

	downloadFile(item): void {
		this.fileService.get(item.id).subscribe(response => {
			const fileName = item.name;
			const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
			saveAs(file);
		});
	}
}
