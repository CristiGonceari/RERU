import { Component, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../services/i18n/i18n.service';
import { LoggingService } from '../../services/logging-service/logging.service';
import { NotificationUtil } from '../../util/notification.util';

@Component({
  selector: 'app-delete-logs-modal',
  templateUrl: './delete-logs-modal.component.html',
  styleUrls: ['./delete-logs-modal.component.scss']
})
export class DeleteLogsModalComponent implements OnInit {

  years: number;
  notification = {
    logs: 'Logs',
    error: 'Error',
    deleteLog: 'Logs deleted!',
    anError: 'Error occured!'
  };

  constructor(private activeModal: NgbActiveModal,
    private notificationService: NotificationsService,
    private loggingService: LoggingService,
    public translate: I18nService) { }

  ngOnInit(): void {
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.title.logs'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.body.success.delete-log'),
      this.translate.get('notification.body.error'),
    ]).subscribe(([logs, error, deleteLog, anError]) => {
      this.notification.logs = logs;
      this.notification.error = error;
      this.notification.deleteLog = deleteLog;
      this.notification.anError = anError;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }
 
  close(): void {
		this.activeModal.close();
	}
 
 deleteLogs(){
  this.loggingService.deleteLogs(this.years).subscribe((res) => {
    this.notificationService.success(this.notification.logs, this.notification.deleteLog, NotificationUtil.getDefaultMidConfig());
    this.close()
  }, (error) => {
    this.notificationService.error(this.notification.error, this.notification.anError, NotificationUtil.getDefaultMidConfig());
     this.close()
  })
}
 
}
