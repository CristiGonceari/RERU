import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { I18nService } from '../i18n/i18n.service';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class PrintTableService {
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];
  filters: any = {};
  abstractService: any

  headersHtml: any;

  constructor(private modalService: NgbModal,
    public translate: I18nService) { }

  getHeaders(abstractService: any, title: string, headersDto: string[], filters: any, document: Document, elementName?: string): void {
    this.init(abstractService, filters)
    this.translateData();

    this.headersHtml = document.getElementsByTagName('th');

    for (let i = 0; i < this.headersHtml.length - 1; i++) {
      this.headersToPrint.push({ value: headersDto[i], label: this.headersHtml[i].innerHTML, isChecked: true })
    }

    let printData = {
      tableName: title,
      fields: this.headersToPrint,
      orientation: 2,
      ...this.filters
    };

    const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.tableData = printData;
    modalRef.componentInstance.translateData = this.printTranslates;
    modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
    this.headersToPrint = [];
  }

  translateData(): void {
    this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
    forkJoin([
      this.translate.get('print.print-table'),
      this.translate.get('print.print-msg'),
      this.translate.get('print.sorted-by'),
      this.translate.get('button.cancel')
    ]).subscribe(
      (items) => {
        for (let i = 0; i < this.printTranslates.length; i++) {
          this.printTranslates[i] = items[i];
        }
      }
    );
  }

  printTable(data): void {
    this.downloadFile = true;
    try {
      this.abstractService.print(data).subscribe(response => {
        if (response) {
          const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -2);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], data.tableName.trim(), { type: response.body.type });
          saveAs(file);
          this.downloadFile = false;
        }
      }, () => this.downloadFile = false);
    } catch (error) {
      console.log("PrintTableService => PintTableERROR:", error);
    }

  }

  init(abstractService: any, filters: any) {
    this.abstractService = abstractService;
    this.filters = filters;
  }
}
