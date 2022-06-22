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

  isRunningProcess: boolean = false;
  cancelRequest: boolean = false;

  constructor(
    private activeModal: NgbActiveModal,
    private getBulkProgressHistoryService: GetBulkProgressHistoryService,
    private testService: TestService,
  ) { }

  ngOnInit(): void {
    this.getBulkProgressHistoryService.getProgressHistory().subscribe(res => {
      this.processesData = res.data,
      this.recivedData = true;
      this.checkIfAreAnyNotDone(this.processesData);
    })

  }

  close(): void {
    this.activeModal.close(this.cancelRequest);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  closeAllProcesses(){
    this.getBulkProgressHistoryService.closeAllProcesses().subscribe();
    this.getBulkProgressHistoryService.cancelRequest(this.cancelRequest);
    this.isRunningProcess = false;
  }

  checkIfAreAnyNotDone(items: any){
    items.forEach(element => {
      if(element.isDone == false){
       this.isRunningProcess = true
       this.cancelRequest = true
      }
    });
  }

  getResultFile(fileId){
    this.testService.getImportResult(fileId).subscribe(response => {
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
