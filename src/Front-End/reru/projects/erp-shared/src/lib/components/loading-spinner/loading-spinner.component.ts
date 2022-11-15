import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent implements OnInit {
  @Input() height: string;
  @Input() width: string;
  @Input() borderWidth: string;
  @Input() colorClass: string;
  @Input() contentHeight: string;
  @Input() contentWidth: string;
  classes: any = {};
  styles: any = {};
  contentStyles: any = {};

  ngOnInit() {
    this.height = this.height ?? '2rem';
    this.width = this.width ?? '2rem';

    this.addClasses();
    this.addStyles();
    this.addContentStyles();
  }

  addStyles(): void {
    if (this.height) {
      this.styles['height'] = this.height;
    }

    if (this.width) {
      this.styles['width'] = this.width;
    }

    if (this.borderWidth) {
      this.styles['border-width'] = this.borderWidth;
    }
  }

  addClasses(): void {
    if (this.colorClass) {
      this.classes[this.colorClass] = true;
    }
  }

  addContentStyles(): void {
    if (this.contentHeight) {
      this.contentStyles['height'] = this.contentHeight;
    }

    if (this.contentWidth) {
      this.contentStyles['width'] = this.contentWidth;
    }
  }
}
