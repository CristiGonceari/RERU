import { Component } from '@angular/core';
import { ReportsService } from '../../../utils/services/reports.service';
import { saveAs } from 'file-saver';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  isLoadingButton: boolean;
  constructor(private reportService: ReportsService) {}

  print(data): void {
    this.isLoadingButton = true;
    const request = ObjectUtil.preParseObject({
      type: data.type == 0 ? null : data.type,
      contractorLastName: data.contractorLastName,
      contractorName: data.contractorName,
      name: data.name,
      fromDate: data.fromDate,
      toDate: data.toDate
    });
    this.reportService.print(request).subscribe(response => {
      const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
      this.isLoadingButton = false;
    });
  }
}
