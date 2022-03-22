import { Component, OnInit } from '@angular/core';
import { Contractor } from '../../utils/models/contractor.model';
import { PermissionModel } from '../../utils/models/permission.model';
import { ContractorProfileService } from '../../utils/services/contractor-profile.service';
import { ContractorService } from '../../utils/services/contractor.service';
import { InitializerUserProfileService } from '../../utils/services/initializer-user-profile.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {
  isLoading: boolean = true;
  avatarIsLoading: boolean = true;
  contractor: Contractor;
  avatarBase64: string;
  acronym: string;
  isNotFound: boolean;
  permissions: PermissionModel;
  fileId: string;

  constructor(private contractorProfileService: ContractorProfileService,
              private contractorService: ContractorService,
              private initializerProfileService: InitializerUserProfileService) { }

  ngOnInit(): void {
    this.retrieUserProfile();
  }

  retrieUserProfile(): void {
    this.initializerProfileService.get().subscribe(response => {
      if (!response || !response.data) return;

      const profile = response.data;
      if (profile && profile.contractorId) {
        this.retrievePermissions();
        return;
      }
  
      this.isNotFound = true;
      this.isLoading = false;
    }, () => {
      this.isNotFound = true;
      this.isLoading = false;
    });
  }

  retrieveProfile(): void {
    this.contractorProfileService.getProfile({}).subscribe((response: any) => {
      this.contractor = response.data;
      this.contractorService.contractor.next(response.data);
      this.setAcronym();
      this.isLoading = false;
    }, () => {}, () => { 
      this.isLoading = false;
    })
    this.contractorProfileService.getProfileAvatar({}).subscribe((response: any) =>{
      this.fileId = response.data.mediaFileId;
      this.avatarIsLoading = false;
    })
  }

  retrievePermissions(): void {
    this.contractorProfileService.getPermissions().subscribe(response => {
      this.permissions = response.data;
      this.isNotFound = this.permissions.getGeneralData ? false : true;
      this.retrieveProfile();
    }, () => {}, () => {
      if (this.isNotFound) {
        this.isLoading = false;
      }
    })
  }

  setAcronym(): void {
    if (this.contractor) {
      this.acronym = `${this.contractor.firstName[0]} ${this.contractor.lastName[0]}`;
    }
  }

  onOutletLoaded(component) {
    component.contractor = this.contractor;
    component.permissions = this.permissions;
  }
}
