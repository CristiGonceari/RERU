import { Component } from '@angular/core';
import { GuideService } from '../../../utils/services/quide/guide.service';
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
  mobileButtonLength: string = "100%";

  constructor(private guideService: GuideService) { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
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
