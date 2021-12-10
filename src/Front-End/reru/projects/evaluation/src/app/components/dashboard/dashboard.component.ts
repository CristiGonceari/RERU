import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './../../utils/services/user-profile/user-profile.service';
import { saveAs } from 'file-saver';
import { CloudFileService } from '../../utils/services/cloud-file/cloud-file.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { PaginationModel } from '../../utils/models/pagination.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  profile;
  isLoading: boolean = true;
  fileId;
  seIncarca: boolean = true;
  seIncarca1: boolean = true;
  seIncarca3: boolean = true;
  isLoadingTable: boolean = true;
  fileType;
  files:[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  lastId;
  uploadFiles;
  fileIdForDelete;

  constructor(private userService: UserProfileService,
              private fileService : CloudFileService,
              private notificationService: NotificationsService,
    ) { }

  ngOnInit(): void {
    this.retrieveProfile();
    this.getDemoList();
  }

  retrieveProfile(): void {
    this.userService.getCurrentUser().subscribe(response => {
      this.profile = response;
      this.isLoading = false;
    })
  }

  downloadFile(item): void {
    this.seIncarca = false;
    this.fileService.get(item).subscribe(response => {
      const fileName = response.name;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
      this.seIncarca = true;
    });
  }

  deleteFile(id):void
  {
    this.fileService.delete(id).subscribe(res => {
      this.notificationService.success('Success', 'Was deleted', NotificationUtil.getDefaultConfig());
      this.getDemoList();
    })
    
  }
  
  uploadFile(): void
  {
    this.seIncarca1 = false;
    const request = new FormData();
    request.append('File', this.uploadFiles);
    request.append('Type', this.fileType);
    this.fileService.create(request).subscribe(res => {
      this.lastId = res.data;
      this.notificationService.success('Success', 'Fișier adăugat!', NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
    }, error =>
    {
      this.notificationService.warn('Error', NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
    })
  }
  
  onFileChange(event){
    this.uploadFiles = event.target.files[0];
  }

  getDemoList(data: any = {}) {

		this.fileService.list().subscribe(
			res => {
				if (res && res.data) {
					this.files = res.data;
					this.pagedSummary = res.data.pagedSummary;
					this.isLoadingTable = false;
				}
			}
		)
	}
  
}
