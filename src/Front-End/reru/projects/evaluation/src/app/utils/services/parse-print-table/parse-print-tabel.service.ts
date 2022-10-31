import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { TablePrintFileNameEnum } from '../../enums/table-print-name.enum';

@Injectable({
  providedIn: 'root'
})
export class ParsePrintTabelService {

  tabelsFileNames = TablePrintFileNameEnum;
  constructor(public translate: TranslateService) { }

  parseFileName(tableName, fileName): string {
    if (this.translate.currentLang == 'ro') {
     fileName = tableName.replace(/_/g, "");
    } else if (this.translate.currentLang == 'ru') {
      fileName = tableName.replace(/_/g, "");
    } else {
      fileName = fileName.replace(/_/g, "");
    }
    console.warn("fileName:", fileName)
    return fileName.replace(/_/g, "");
  }
}
