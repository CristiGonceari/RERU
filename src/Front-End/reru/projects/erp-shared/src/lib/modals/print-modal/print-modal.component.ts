import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'erp-shared-print-modal',
  templateUrl: './print-modal.component.html',
  styleUrls: ['./print-modal.component.scss'],
})
export class PrintModalComponent implements OnInit {
  exportTableFormat: any[] = [
    {
      label: 'Pdf',
      value: 0,
    },
    {
      label: 'Excel',
      value: 1,
    },
    {
      label: 'Xml',
      value: 2,
    },
  ];

  selectedFormat: number = 0;
  selectedFields = [];

  @Input() tableData = {
    tableName: '',
    fields: [],
    orientation: 2,
    name: '',
    tableExportFormat: 0,
  };
  
  @Input() translateData: Array<string>;

  constructor(private activeModal: NgbActiveModal) {}

  ngOnInit(): void {
    for (let i = 0; i < this.tableData.fields.length; i++) {
      this.selectedFields.push(this.tableData.fields[i]);
    }
  }

  setOrientation(event): void {
    this.tableData.orientation = event;
    this.print();
  }

  onItemChange(event, item): void {

    item.isChecked = event.target.checked;
  }

  print(): void {

    const fields = this.tableData.fields.filter((el) => el.isChecked == true);
    
    this.tableData.fields = fields;
    this.tableData.tableExportFormat = this.selectedFormat;
    this.activeModal.close(this.tableData);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
