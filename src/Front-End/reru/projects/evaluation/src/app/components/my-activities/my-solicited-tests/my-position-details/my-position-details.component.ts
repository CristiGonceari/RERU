import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { SolicitedVacantPositionUserFileService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-user-file/solicited-vacant-position-user-file.service';
import { SolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/solicited-test.model';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { saveAs } from 'file-saver';

@Component({
    selector: 'app-my-position-details',
    templateUrl: './my-position-details.component.html',
    styleUrls: ['./my-position-details.component.scss']
})

export class MyPositionDetailsComponent implements OnInit {
  date: Date;
  solicitedTestId;
  candidatePositionId;

  solicitedTest: SolicitedTest = new SolicitedTest();

  isLoading: boolean = true;
  isLoadingMedia: boolean = true;

  userFiles: any[] = [];
  mobileButtonLength: string = "100%";

  constructor(
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    public activatedRoute: ActivatedRoute,
    private solicitedVacantPositionUserFileService: SolicitedVacantPositionUserFileService) { }

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
   return { userId, solicitedVacantPositionId, candidatePositionId }  
  }

  ceckFileNameLength(fileName: string) {
    return fileName.length <= 20 ? fileName : fileName.slice(0, 20) + "...";
  }

  backClicked() {
    this.location.back();
  }
}
