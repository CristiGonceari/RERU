import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { AddEditSolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/add-edit-solicited-test.model';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { CandidatePositionService } from 'projects/evaluation/src/app/utils/services/candidate-position/candidate-position.service';
import { EventCandidatePositionService } from 'projects/evaluation/src/app/utils/services/event-candidate-position/event-candidate-position.service';
import { SolicitedVacantPositionUserFileService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-user-file/solicited-vacant-position-user-file.service';
import { ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-add-edit-solicited-test',
  templateUrl: './add-edit-solicited-test.component.html',
  styleUrls: ['./add-edit-solicited-test.component.scss']
})
export class AddEditSolicitedTestComponent implements OnInit {
  showCard: boolean = false;
  isEdit: boolean = false;
  isDeleting: boolean = false;

  selected;

  eventsList: [] = [];
  selectActiveTests: [] = [];
  user = new SelectItem();
  event = new SelectItem();
  testTemplate = new SelectItem();
  date: Date;
  search: string;
  title: string;
  description: string;
  isLoading: boolean = true;
  solicitedPositionId;
  solicitedTest: AddEditSolicitedTest;
  candidatePositions = new SelectItem();

  uploadForm: FormGroup;

  eventsWithTestList: any[] = [];
  requiredDocumentsList: any[] = [];

  files: any[] = [];
  userFiles: any;


  constructor(
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    public activatedRoute: ActivatedRoute,
    private candidatePosition: CandidatePositionService,
    private eventCandidatePositionService: EventCandidatePositionService,
    private solicitedVacantPositionUserFileService: SolicitedVacantPositionUserFileService
  ) { }

  ngOnInit(): void {
    this.initData();

    if (this.solicitedPositionId == null) {
      this.retrievePositions();
    }
  }

  initData(): void {
    this.solicitedPositionId = this.activatedRoute.snapshot.paramMap.get('id');

    if (this.solicitedPositionId != null) this.getSolicitedPosition(this.solicitedPositionId)
  }

  getSolicitedPosition(solicitedPositionId) {
    this.isEdit = true;
    this.candidatePosition.getPositionValues({ id: solicitedPositionId }).subscribe((res) => (
      this.candidatePositions = res.data,
      this.selected = this.candidatePositions[0],
      this.getEventsAndDocuments(),
      this.isLoading = false
    ));
  }

  getFiles() {
    if (this.isEdit) {
      this.solicitedVacantPositionUserFileService.getList({ solicitedVacantPositionId: this.solicitedPositionId, candidatePositionId: +this.selected.value }).subscribe(res => {
        this.userFiles = res.data;
        this.isDeleting = false;
      })
    }
  }

  setFile(event, index, requiredDocumenId): void {
    const file = event.target.files[0];
    const fileToAdd = this.parseFileToAdd(file, requiredDocumenId)

    if (this.files[index] !== undefined) {
      this.files[index] = fileToAdd;
    } else {
      this.files.push(fileToAdd);
    }
  }

  GetFile(fileId: string) {
    this.solicitedVacantPositionUserFileService.get(fileId).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        const fileNameParsed = fileName.substring(1, fileName.length - 1);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileNameParsed, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

  getEventsAndDocuments() {
    if (this.isEdit) {
      this.eventCandidatePositionService.getEventVacandPostition(+this.selected.value).subscribe(res => {
        if (res && res.data) {
          this.eventsWithTestList = res.data.events;
          this.requiredDocumentsList = res.data.requiredDocuments;
          this.getFiles(),

            this.showCard = true;
        } else {
          this.showCard = false;
        }
      })
    } else {
      this.eventCandidatePositionService.getEventVacandPostition(+this.candidatePositions.value).subscribe(res => {
        if (res && res.data) {
          this.eventsWithTestList = res.data.events;
          this.requiredDocumentsList = res.data.requiredDocuments;
          this.getFiles(),

            this.showCard = true;
        } else {
          this.showCard = false;
        }
      })
    }
  }

  retrievePositions() {
    this.candidatePosition.getPositionValues({}).subscribe((res) => (
      this.candidatePositions = res.data,
      this.selected = this.candidatePosition[0],
      console.log("this.selected", this.selected),
      this.isLoading = false
    ));
  }

  onSave(): void {
    if (this.isEdit) {
      this.edit();
    } else {
      this.add();
    }
  }

  parseFileToAdd(file, requiredDocumenId) {
    return {
      file: {
        file: file,
        requiredDocumenId: requiredDocumenId
      }
    };
  }

  parse() {
    return {
      data: {
        solicitedTestStatus: 0,
        candidatePositionId: this.candidatePositions.value || 0
      }
    };
  }

  parseToEdit() {
    return {
      data: {
        id: this.solicitedPositionId,
        candidatePositionId: +this.selected.value,
        solicitedTestStatus: 0
      }
    }
  }

  add() {
    this.solicitedTestService.addMySolicitedTest(this.parse()).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('solicited-test.succes-add-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });

      this.isLoading = true;

      if (this.files.length > 0) {
        this.uploadFiles(res);
      } else {
        this.isLoading = false
        this.backClicked();
      }
    });
  }

  edit() {
    this.solicitedTestService.editMySolicitedTest(this.parseToEdit()).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('solicited-test.succes-add-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });

      this.isLoading = true;
      if (this.files.length > 0) {
        this.uploadFiles(res);
      } else {
        this.isLoading = false
        this.backClicked();
      }
    });
  }

  uploadFiles(res) {
    this.files.forEach(el => {
      if (this.files[this.files.length - 1] === el) {
        let request = new FormData();
        request = this.parseFiles(request, res, el);

        if (el.file.file != null) {
          this.solicitedVacantPositionUserFileService.create(request).subscribe(res => {
            this.backClicked();
          });
        }

      } else {
        let request = new FormData();
        request = this.parseFiles(request, res, el);
        request = this.parseFiles(request, res, el);

        if (el.file.file != null) {
          this.solicitedVacantPositionUserFileService.create(request).subscribe();
        }
      }
    })
  }

  parseFiles(request: FormData, res, el) {
    const fileType = '5';
    request.append('UserProfileId', res.data.userProfileId);
    request.append('SolicitedVacantPositionId', res.data.solicitedVacantPositionId);
    request.append('RequiredDocumentId', el.file.requiredDocumenId);
    request.append('File.File', el.file.file);
    request.append('File.Type', fileType);

    return request;
  }

  deleteFile(fileId) {
    this.isDeleting = true;
    this.solicitedVacantPositionUserFileService.deleteFile(fileId).subscribe(() => {
      this.getFiles()
    })
  }

  ceckFileNameLength(fileName: string) {
    return fileName.length <= 20 ? fileName : fileName.slice(0, 20) + "...";
  }

  backClicked() {
    this.location.back();
  }
}
