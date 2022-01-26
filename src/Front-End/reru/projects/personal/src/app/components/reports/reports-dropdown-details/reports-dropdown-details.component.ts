import { Component, Input, Output, EventEmitter, OnInit} from '@angular/core';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-reports-dropdown-details',
  templateUrl: './reports-dropdown-details.component.html',
  styleUrls: ['./reports-dropdown-details.component.scss']
})
export class ReportsDropdownDetailsComponent{

  @Input() contractorId;
  @Input() viewLink: string[];
  @Input() index: number;
  @Input() file: any;

  @Output() refresh: EventEmitter<void> = new EventEmitter<void>();

  constructor(private fileService: FileService) { }
  
  downloadFile(item): void {
    this.fileService.get(item.id).subscribe(response => {
      const fileName = item.name;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

}
