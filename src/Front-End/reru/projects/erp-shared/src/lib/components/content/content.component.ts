import { Component, Input } from '@angular/core';

@Component({
  selector: 'erp-shared-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.scss']
})
export class ContentComponent {
  @Input() isFluidContainer: boolean;
  @Input() isFullSizeContainer: boolean;
  @Input() classes: string;
}
