import { Component, OnInit } from '@angular/core';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { SolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/solicited-test.model';
import { Location } from '@angular/common';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { ActivatedRoute } from '@angular/router';
import { SolicitedVacantPositionUserFileService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-user-file/solicited-vacant-position-user-file.service';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ReviewSolicitedVacandPositionModalComponent } from 'projects/evaluation/src/app/utils/modals/review-solicited-vacand-position-modal/review-solicited-vacand-position-modal.component';
import { SolicitedVacantPositionEmailMessageService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-email-message/solicited-vacant-position-email-message.service';

@Component({
  selector: 'app-approve-solicited-test',
  templateUrl: './approve-solicited-test.component.html',
  styleUrls: ['./approve-solicited-test.component.scss']
})
export class ApproveSolicitedTestComponent implements OnInit {
  date: Date;
  title: string;
  description: string;
  solicitedTestId;
  candidatePositionId;

  solicitedTest: SolicitedTest = new SolicitedTest();

  isLoading: boolean = true;
  isLoadingMedia: boolean = true;

  messageText = "";

  userFiles: any[] = [];

  constructor(
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    public activatedRoute: ActivatedRoute,
    private modalService: NgbModal,
    private solicitedVacantPositionUserFileService: SolicitedVacantPositionUserFileService,
    private solicitedVacantPositionEmailMessageService: SolicitedVacantPositionEmailMessageService
  ) { }

  ngOnInit(): void {
    this.initData();
  }

  getFile(fileId: string) {
    this.solicitedVacantPositionUserFileService.get(fileId).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
        const fileNameParsed = fileName.replace(/[&\/\\#,+()$~%'":*?<>{}]/g, '');
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileNameParsed, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

  initData(): void {
    this.solicitedTestId = this.activatedRoute.snapshot.paramMap.get('id');
    this.candidatePositionId = this.activatedRoute.snapshot.paramMap.get('positionId');
    if (this.solicitedTestId != null) this.getSolicitedPosition(this.solicitedTestId, this.candidatePositionId)
    else this.isLoading = false;
  }

  getSolicitedPosition(id, positionId) {
    this.solicitedTestService.getSolicitedTest({ id: id, candidatePositionId: positionId })
      .subscribe(res => {
        if (res && res.data) {
          this.solicitedTest = res.data;
          this.date = res.data.solicitedTime;
          this.isLoading = false;
        }

        this.solicitedVacantPositionUserFileService
          .getList(this.parse(this.solicitedTest.userProfileId, this.solicitedTestId, this.solicitedTest.candidatePositionId))
          .subscribe(res => {
            this.userFiles = res.data;
            this.isLoadingMedia = false;
          })
      });
  }

  parse(userId: number, solicitedVacantPositionId: number, candidatePositionId: number) {
    let request = {
      userId,
      solicitedVacantPositionId,
      candidatePositionId
    }

    return request
  }

  next() {
    const modalRef: any = this.modalService.open(ReviewSolicitedVacandPositionModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.userEmail = this.solicitedTest.email;
    modalRef.componentInstance.userName = this.solicitedTest.userProfileName;
    modalRef.componentInstance.solicitedTestId = this.solicitedTestId;
    modalRef.result.then(() => {
      this.sendEmailAndChangeStatus(modalRef.result.__zone_symbol__value)
    }, () => { });
  }

  sendEmailAndChangeStatus(data) {
    let request = {
      emailMessage: data.EmailMessage,
      result: data.messageEnum,
      solicitedVacantPositionId: this.solicitedTestId
    }

    this.solicitedVacantPositionEmailMessageService.changeStatusAndSendEmail(request).subscribe(() => this.backClicked());
  }

  ceckFileNameLength(fileName: string) {
    return fileName.length <= 20 ? fileName : fileName.slice(0, 20) + "...";
  }

  backClicked() {
    this.location.back();
  }
}
