import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewEncapsulation } from '@angular/core';
import { EnumStringTranslatorService } from '../../services/enum-string-translator.service';
import { TestStatusEnum } from '../../enums/test-status.enum';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { NotificationsService } from 'angular2-notifications';
import { PrintTemplateService } from '../../services/print-template/print-template.service';
import { CandidatePositionService } from '../../services/candidate-position/candidate-position.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-view-position-diagram-modal',
  templateUrl: './view-position-diagram-modal.component.html',
  styleUrls: ['./view-position-diagram-modal.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class ViewPositionDiagramModalComponent implements OnInit {
  eventsDiagram = [];
  usersDiagram = [];
  testTemplates = [];
  positionId;
  fileName: string;
  fileStatus = { requestType: '', percent: 1 }
  isLoadingMedia: boolean = false;
  title: string;
  description: string;
  no: string;
  yes: string;
  status = TestStatusEnum;
  isActive: boolean = true;

  constructor(private activeModal: NgbActiveModal,
    private enumStringTranslatorService: EnumStringTranslatorService,
    public translate: I18nService,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService,
    private positionService: CandidatePositionService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
  }

  translateResultValue(item) {
    return this.enumStringTranslatorService.translateTestResultValue(item);
  }

  openAddTest(value) {
    let data = {
      isOpenAddTest: true,
      selectedEventId: value.eventId,
      selectedTestTemplateId: value.testTemplateId
    }

    this.activeModal.close(data);
  }

  close(): void {
    this.activeModal.dismiss();
  }

  printPositionDiagram() {
    this.isLoadingMedia = true;

    this.printService.getPositionDiagramPdf(this.positionId).subscribe((response: any) => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('position.success-download'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.reportProggress(response, true);
    },
      (error) => {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('position.error-download'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoadingMedia = false;
      });
  }

  getPositionDiagramExcel() {
    this.isLoadingMedia = true;

    const params = {
      positionId: this.positionId
    }

    this.positionService.exportPositionDiagram(params).subscribe((event) => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('position.success-download'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.reportProggress(event, false);
    },
      (error) => {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('position.error-download'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoadingMedia = false;
      })
  }

  private reportProggress(httpEvent: HttpEvent<Blob>, isPdf): void {
    let fileName;

    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.fileStatus.percent = 1;
        break;
      case HttpEventType.UploadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'În progres...')
        break;
      case HttpEventType.DownloadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'În progres...')
        break;
      case HttpEventType.Response:
        this.fileStatus.requestType = "Terminat";
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());

        if (isPdf) {
          fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

          if (httpEvent.body.type === 'application/pdf') {
            fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
          }
        } else {
          fileName = httpEvent.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
        }

        const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
        const file = new File([blob], fileName, { type: httpEvent.body.type });
        saveAs(file);
        this.isLoadingMedia = false;
        break;
    }
  }

  updateStatus(loaded: number, total: number | undefined, requestType: string) {
    this.fileStatus.requestType = requestType;
    this.fileStatus.percent = Math.round(100 * loaded / total);
  }
  routeToUserProfileSolicitedTests(userId) {
    this.router.navigate(['../../../../user-profile/', userId, 'solicited-tests'], { relativeTo: this.route });
    this.close();
  }
}
