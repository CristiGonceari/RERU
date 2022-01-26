import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { saveAs } from 'file-saver';
import { GuideService } from '../../utils/services/guide.service'
import { InitializerUserProfileService } from '../../utils/services/initializer-user-profile.service';
@Component({
  selector: 'app-guide',
  templateUrl: './guide.component.html',
  styleUrls: ['./guide.component.scss']
})
export class GuideComponent implements OnInit{

  @Output() update: EventEmitter<void> = new EventEmitter<void>();
 
  isLoadingButton: boolean;
  profile;
  isLoading: boolean = true;

  constructor(private guideService: GuideService, 
    private profileService: InitializerUserProfileService) { }

  ngOnInit(): void{
    this.retrieveProfile();
  }

  retrieveProfile(): void {
    this.profileService.profile.subscribe(response => {
      this.profile = response;
      this.isLoading = false;
    })
  }

  downloadFile(): void {
		this.guideService.get().subscribe((response : any) => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
      
      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
		});
	}

}
