import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './../../utils/services/user-profile/user-profile.service';
import { saveAs } from 'file-saver';
import { CloudFileService } from '../../utils/services/cloud-file/cloud-file.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';

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
  fileType;
  files;
  lastId;

  constructor(private userService: UserProfileService,
              private fileService : CloudFileService,
              private notificationService: NotificationsService,
    ) { }

  ngOnInit(): void {
    this.retrieveProfile();
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
  uploadFile(): void
  {
    this.seIncarca1 = false;
    const request = new FormData();
    request.append('File', this.files);
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
    this.files = event.target.files[0];
  }
  
}
