import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.scss']
})
export class ContentComponent implements OnInit {
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
