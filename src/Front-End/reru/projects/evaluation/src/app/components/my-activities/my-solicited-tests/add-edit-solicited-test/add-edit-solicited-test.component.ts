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
import { FormBuilder, FormGroup } from '@angular/forms';
import { saveAs } from 'file-saver';
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-add-edit-solicited-test',
  templateUrl: './add-edit-solicited-test.component.html'
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

  addEditSolicitedPositionForm: FormGroup;

  eventsWithTestList: any[] = [];
  requiredDocumentsList: any[] = [];

  files: any[] = [];
  userFiles: any;

  isLoadingMedia: boolean = false;

  filenames: any;
  fileName: string;
  fileStatus = { requestType: '', percent: 1 }

  constructor(
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    public activatedRoute: ActivatedRoute,
    private candidatePosition: CandidatePositionService,
    private eventCandidatePositionService: EventCandidatePositionService,
    private solicitedVacantPositionUserFileService: SolicitedVacantPositionUserFileService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.initData();

    if (this.solicitedPositionId == null) {
      this.retrievePositions();
    }
  }

  initData(): void {
    this.solicitedPositionId = this.activatedRoute.snapshot.paramMap.get('id');
    this.initForm();

    if (this.solicitedPositionId != null) this.getSolicitedPosition(this.solicitedPositionId)
  }

  initForm(data?, id?){
      this.addEditSolicitedPositionForm = this.fb.group({
        id: this.fb.control(id || null, []),
        candidatePositionId: this.fb.control(data || null, [])
      });
  }

  getSolicitedPosition(solicitedPositionId) {
    this.isEdit = true;
    this.candidatePosition.getPositionValues({ id: solicitedPositionId }).subscribe(res => {
      this.candidatePositions = res.data;
      this.selected = this.candidatePositions[0];
      this.getEventsAndDocuments();
      this.isLoading = false;
      this.initForm(res.data[0].value, solicitedPositionId);
  });
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
    const file = event;
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
        const fileNameParsed = fileName.replace(/[&\/\\#,+()$~%'":*?<>{}]/g, '');
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
      this.eventCandidatePositionService.getEventVacandPostition(this.addEditSolicitedPositionForm.value.candidatePositionId).subscribe(res => {
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
      this.isLoading = false
    ));
  }

  onSave(): void {
    this.isLoading = true;
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

  parse(form) {
    return {
      data: {
        solicitedTestStatus: 0,
        candidatePositionId: form.value.candidatePositionId || 0
      }
    };
  }

  parseToEdit(form) {
    return {
      data: {
        id: form.value.id ,
        candidatePositionId: form.value.candidatePositionId,
        solicitedTestStatus: 0
      }
    }
  }

  add() {
    this.solicitedTestService.addMySolicitedTest(this.parse(this.addEditSolicitedPositionForm)).subscribe(res => {
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
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false
        this.backClicked();
      }
    }, err => {
      this.isLoading = false
    });
  }

  edit() {
    this.solicitedTestService.editMySolicitedTest(this.parseToEdit(this.addEditSolicitedPositionForm)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('solicited-test.succes-edit-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });

      this.isLoading = true;
      if (this.files.length > 0) {
        this.uploadFiles(res);
      } else {
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false
        this.backClicked();
      }
    }, err => {
      this.isLoading = false
    });
  }

  uploadFiles(res) {
    this.isLoading = false;
    this.files.forEach(el => {
      if (this.files[this.files.length - 1] === el) {
        let request = new FormData();
        request = this.parseFiles(request, res, el);

        if (el.file.file != null) {
          this.solicitedVacantPositionUserFileService.create(request).subscribe(res => {
            this.reportProggress(res);
            // this.backClicked();
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

  private reportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.isLoadingMedia = true;
        this.fileStatus.percent = 1;
        break;
      case HttpEventType.UploadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
        break;
      case HttpEventType.DownloadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
        break;
      case HttpEventType.Response:
        this.fileStatus.requestType = "Done";
        this.backClicked();
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoadingMedia = false;
        break;
    }
  }

  updateStatus(loaded: number, total: number | undefined, requestType: string) {
    this.fileStatus.requestType = requestType;
    this.fileStatus.percent = Math.round(100 * loaded / total);
  }

  cantAdd() {
    return this.candidatePositions == undefined;
  }
}
