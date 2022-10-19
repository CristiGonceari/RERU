import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class EnumStringTranslatorService {

  constructor(public translate: TranslateService) { }

  translateTestResultValue(a) {
    if (this.translate.currentLang == 'en') {
      return a.replace('NoResult', 'No result')
        .replace('NotPassed', 'Not passed')
        .replace('Passed', 'Passed')
        .replace('NotAble', 'Not able')
        .replace('Able', 'Able')
        .replace('Accepted', 'Accepted')
        .replace('Rejected', 'Rejected')
        .replace('Recommended', 'Recommended/Not recommended');
    } else if (this.translate.currentLang == 'ru') {
      return a.replace('NoResult', 'Без pезультата')
        .replace('NotPassed', 'Не пройдено')
        .replace('Passed', 'Пройдено')
        .replace('NotAble', 'Hе подходит')
        .replace('Able', 'Подходит')
        .replace('Accepted', 'Принят')
        .replace('Rejected', 'Oтклонен')
        .replace('Recommended', 'Pекомендован/Не рекомендован');
    } else {
      return a.replace('NoResult', 'Fără rezultat')
        .replace('NotPassed', 'Nesusținut')
        .replace('Passed', 'Susținut')
        .replace('NotAble', 'Inapt')
        .replace('Able', 'Apt')
        .replace('Accepted', 'Admis')
        .replace('Rejected', 'Respins')
        .replace('Recommended', 'Se recomandă/Nu se recomandă');
    }
  }
}
