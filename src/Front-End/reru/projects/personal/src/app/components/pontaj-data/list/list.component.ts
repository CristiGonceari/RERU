import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TimeSheetTableValuesService } from '../../../utils/services/time-sheet-table-values.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { SearchByDateComponent } from './search-by-date/search-by-date.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements AfterViewInit {
  @ViewChild(SearchByDateComponent) searchByDateComponent!: SearchByDateComponent;

  isLoading: boolean = false;
  filters: any = {};
   
  constructor(
    private timeSheetTableValuesService: TimeSheetTableValuesService,
  ) { }

  ngAfterViewInit() {
    setTimeout(() => {
      this.searchByDateComponent.sendData();
    }, 10);
  }

  print(data): void {
    this.isLoading = true;
    data = ObjectUtil.preParseObject({
      ...data,
      page: data.page,
      contractorName: data.contractorName,
      fromDate: data.fromDate,
      toDate: data.toDate,
      itemsPerPage: data.itemsPerPage,
      ...this.filters
    });
    this.timeSheetTableValuesService.print(data).subscribe(response => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
      this.isLoading = false;
    });
  }

  printAll(data): void {
    this.isLoading = true;
    data = ObjectUtil.preParseObject({
      ...data,
      fromDate: data.fromDate,
      toDate: data.toDate,
      ...this.filters
    });
    this.timeSheetTableValuesService.printAll(data).subscribe(response => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
      this.isLoading = false;
    });
  }
  
  deleteTableDatas(data): void{
    data = ObjectUtil.preParseObject({
      ...data,
      fromDate: data.fromDate,
      toDate: data.toDate
    });
     this.timeSheetTableValuesService.deleteTableValues(data).subscribe(() => {
       this.ngAfterViewInit();
     })
  }
}

