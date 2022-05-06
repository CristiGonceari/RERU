import { Component } from '@angular/core';
import { GuideService } from '../../../utils/services/guide.service';
import { TryLongRequestService } from '../../../utils/services/try-long-request.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-faq-list',
  templateUrl: './faq-list.component.html', 
  styleUrls: ['./faq-list.component.scss']
})
export class FaqListComponent {
  title: string;
  isLoadingButton: boolean;
  isLoading: boolean = true;

  constructor(private guideService: GuideService,
              private tryLongRequestService: TryLongRequestService
    ) { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  tryLongRequest(){
    this.tryLongRequestService.getLongRequest().subscribe(res => console.log("res", res))
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
