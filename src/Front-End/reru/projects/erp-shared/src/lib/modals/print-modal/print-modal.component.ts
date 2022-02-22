import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'erp-shared-print-modal',
  templateUrl: './print-modal.component.html',
  styleUrls: ['./print-modal.component.scss']
})
export class PrintModalComponent implements OnInit {

  selectedFields = [];
  @Input() tableData = {tableName: '', fields: [], orientation: 2, name: ''};
  @Input() translateData: Array<string>;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    for (let i=0; i<this.tableData.fields.length; i++) {
      this.selectedFields.push(this.tableData.fields[i]);
    }
  }

  setOrientation(event): void {
    this.tableData.orientation = event;
    this.print();
  }

  onItemChange(event, item): void {
    if (event.target.checked === true) {
      this.selectedFields.push(item);
    } else if (event.target.checked === false) {
      this.selectedFields.splice(this.selectedFields.indexOf(item), 1);
    }
  }

  print(): void {
    this.tableData.fields = this.selectedFields;
    this.activeModal.close(this.tableData);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
