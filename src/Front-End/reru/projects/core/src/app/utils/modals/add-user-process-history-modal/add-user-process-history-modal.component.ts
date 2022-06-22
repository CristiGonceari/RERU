import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProcessService } from '../../services/process.service';
import { UserService } from '../../services/user.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-add-user-process-history-modal',
  templateUrl: './add-user-process-history-modal.component.html',
  styleUrls: ['./add-user-process-history-modal.component.scss']
})
export class AddUserProcessHistoryModalComponent implements OnInit {

  processesData: any;
  recivedData: boolean = false;

  constructor(
    private activeModal: NgbActiveModal,
    private processService: ProcessService,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    this.processService.getProgressHistory().subscribe(res => {
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
    this.userService.getImportResult(fileId).subscribe(response => {
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
