import { Component, Input, ViewEncapsulation } from '@angular/core';
import { SafeHtmlPipe } from '../../../../pipes/safe-html.pipe';

@Component({
  selector: 'app-view-icon',
  templateUrl: './view-icon.component.html',
  styleUrls: ['./view-icon.component.scss'],
  providers: [SafeHtmlPipe],
  encapsulation: ViewEncapsulation.None
})
export class ViewIconComponent {
  @Input() module: any;
  constructor() { }

}
