import { Component, OnInit } from '@angular/core';
import { CandidatePositionService } from '../../../utils/services/candidate-position/candidate-position.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-position-details',
  templateUrl: './position-details.component.html',
  styleUrls: ['./position-details.component.scss']
})
export class PositionDetailsComponent implements OnInit {
  isLoading: boolean = true;
  positionName;
  positionId;

  title: string;
  description: string;
  no: string;
  yes: string;

  constructor(private positionService: CandidatePositionService,
    private route: ActivatedRoute,
    public translate: I18nService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    private router: Router) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.positionId = response.id;
        this.get(response.id);
      }
    })
  }

  get(id: number): void {
    this.positionService.get(id).subscribe(response => {
      this.positionName = response.data.name;
      this.isLoading = false;
    });
  }

  openRemoveModal(id: number): void {
    forkJoin([
      this.translate.get('modal.delete'),
      this.translate.get('position.delete-msg'),
      this.translate.get('modal.no'),
      this.translate.get('modal.yes'),
    ]).subscribe(([title, description, no, yes]) => {
      this.title = title;
      this.description = description;
      this.no = no;
      this.yes = yes;
    });
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.title;
    modalRef.componentInstance.description = `${this.description} (${this.positionName})?`;
    modalRef.componentInstance.buttonNo = this.no;
    modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(() => this.removePosition(id, this.positionName), () => { });
  }

  removePosition(id: number, name: string): void {
    this.positionService.delete(id).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('position.success-delete'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, `${this.description} - ${name}`, NotificationUtil.getDefaultMidConfig(),
        this.router.navigate(['positions'])
      );
    });
  }
}
