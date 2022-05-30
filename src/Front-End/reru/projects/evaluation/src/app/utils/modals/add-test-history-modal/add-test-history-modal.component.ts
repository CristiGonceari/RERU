import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GetBulkProgressHistoryService } from '../../services/bulk-progress/get-bulk-progress-history.service';
import { TestService } from '../../services/test/test.service';
import { saveAs } from 'file-saver';


@Component({
  selector: 'app-add-test-history-modal',
  templateUrl: './add-test-history-modal.component.html',
  styleUrls: ['./add-test-history-modal.component.scss']
})
export class AddTestHistoryModalComponent implements OnInit {

  processesData: any;
  recivedData: boolean = false;

  constructor(
    private activeModal: NgbActiveModal,
    private getBulkProgressHistoryService: GetBulkProgressHistoryService,
    private testService: TestService,
  ) { }

  ngOnInit(): void {
    this.getBulkProgressHistoryService.getBulkProgressHistory().subscribe(res => {
      this.processesData = res.data,
      this.recivedData = true;
    })
  }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  getResultFile(fileId){
    this.testService.getBulkImportResult(fileId).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

}
