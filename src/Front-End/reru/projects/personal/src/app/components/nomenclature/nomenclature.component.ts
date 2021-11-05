import { Component } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';

@Component({
  selector: '',
  template: '<router-outlet></router-outlet>'
})
export class NomenclatureComponent {
  constructor(private translate: I18nService) { }
}
