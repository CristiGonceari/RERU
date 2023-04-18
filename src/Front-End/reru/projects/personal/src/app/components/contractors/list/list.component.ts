import { ChangeDetectorRef, Component } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  clearFields: boolean = false;

  constructor(private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  startClearFields()
  {
    this.clearFields = true;
    this.cd.detectChanges();
    this.clearFields = false;
  }
}


