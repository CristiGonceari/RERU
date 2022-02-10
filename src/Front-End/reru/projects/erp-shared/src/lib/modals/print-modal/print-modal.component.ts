import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'erp-shared-print-modal',
  templateUrl: './print-modal.component.html',
  styleUrls: ['./print-modal.component.scss']
})
export class PrintModalComponent implements OnInit {

  selectedFeilds = [];
  disabled: boolean = true;
  @Input() tableData = {tableName: '', fields: [], orientation: 1};
  @Input() translateData: Array<string>;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  setOrientation(event): void {
    this.tableData.orientation = event;
    this.close();
  }

  checkSelection(): void {
    if (this.selectedFeilds.length) this.disabled = false;
  }

  onItemChange(event, item): void {
    if (event.target.checked === true) {
      this.selectedFeilds.push(item);
    } else if (event.target.checked === false) {
      let itemToExclude = event.target.value;
      this.selectedFeilds.splice(this.selectedFeilds.indexOf(itemToExclude), 1);
    }
    this.checkSelection();
  }

  close(): void {
    this.tableData.fields = this.selectedFeilds;
    this.activeModal.close(this.tableData);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
