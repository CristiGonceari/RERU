import { Component, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { LoggingService } from '../../services/logging-service/logging.service';
import { NotificationUtil } from '../../util/notification.util';

@Component({
  selector: 'app-delete-logs-modal',
  templateUrl: './delete-logs-modal.component.html',
  styleUrls: ['./delete-logs-modal.component.scss']
})
export class DeleteLogsModalComponent implements OnInit {

  years: number;

  constructor(private activeModal: NgbActiveModal,
    private notificationService: NotificationsService,
    private loggingService: LoggingService) { }

  ngOnInit(): void {
  }
 
  close(): void {
		this.activeModal.close();
	}
 
 deleteLogs(){
  this.loggingService.deleteLogs(this.years).subscribe((res) => {
    this.notificationService.success('Logs', 'Logs deleted!', NotificationUtil.getDefaultMidConfig());
    this.close()
  }, (error) => {
    this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultMidConfig());
     this.close()
  })
}
 
}
