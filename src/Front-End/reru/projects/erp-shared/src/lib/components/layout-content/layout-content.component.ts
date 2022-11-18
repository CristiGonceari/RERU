import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout-content',
  templateUrl: './layout-content.component.html',
  styleUrls: ['./layout-content.component.scss']
})
export class LayoutContentComponent implements OnInit {
  @Input() hasBackground: boolean;
  @Input() backgroundColor: string;
  styles = { background: '' };
  constructor() { }

  ngOnInit(): void {
    this.addStyles();
  }

  addStyles(): void {
    if (this.hasBackground) {
      this.styles.background = this.backgroundColor;
    }
  }
}
