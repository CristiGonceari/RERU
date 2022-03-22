import { AfterViewInit, ChangeDetectorRef, Component, Output, ViewChild} from '@angular/core';
import { ContractorService } from '../../../utils/services/contractor.service';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin, Observable, of } from 'rxjs';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { MatHorizontalStepper } from '@angular/material/stepper';
import { GeneralDataFormComponent } from './general-data-form/general-data-form.component';
import { BulletinDataFormComponent } from './bulletin-data-form/bulletin-data-form.component';
import { StudiesDataFormComponent } from './studies-data-form/studies-data-form.component';
import { UploadDataFormComponent } from './upload-data-form/upload-data-form.component';
import { ContractorParser } from './add.parser';
import { FileService } from '../../../utils/services/file.service';
import { BulletinService } from '../../../utils/services/bulletin.service';
import { StudyService } from '../../../utils/services/study.service';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { delay } from 'rxjs/operators';
import { DocumentTypeEnum } from '../../../utils/models/document-type.enum';
import { RequestToEmployDataFormComponent } from './request-to-employ-data-form/request-to-employ-data-form.component';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements AfterViewInit {
  @ViewChild(GeneralDataFormComponent) generalDataFormComponent: GeneralDataFormComponent;
  @ViewChild(BulletinDataFormComponent) bulletinDataFormComponent: BulletinDataFormComponent;
  @ViewChild(RequestToEmployDataFormComponent) requestToEmployDataFormComponent: RequestToEmployDataFormComponent;
  @ViewChild(StudiesDataFormComponent) studiesDataFormComponent: StudiesDataFormComponent;
  @ViewChild(UploadDataFormComponent) uploadDataFormComponent: UploadDataFormComponent;
  @ViewChild('stepper') stepper: MatHorizontalStepper;
  isEditable: boolean = false;
  isLoading: boolean;
  isDisabledButton: boolean;
  contractorId: any;
  completedEvents = [];
  queryContractorId: number;

  photoDataFormComponent: boolean;
  fileId: string;
  fileType: string = null;
  attachedFile: File;

  constructor(private contractorService: ContractorService,
              private notificationService: NotificationsService,
              private fileService: FileService,
              private bulletinService: BulletinService,
              private studyService: StudyService,
              private router: Router,
              private route: ActivatedRoute,
              private cd: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.cd.detectChanges();
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    combineLatest([
      this.route.params,
      this.route.queryParams
    ]).subscribe(([params, query]) => {
      if (params.id && query.step) {
        this.contractorId = +params.id;
        this.queryContractorId= +params.id;
        this.bulletinDataFormComponent.bulletinForm.get('contractorId').patchValue(+params.id);
        if (query.step!=0) this.generalDataFormComponent.isLoading = true;
        this.setCompleted(+(query.step));
        of(true).pipe(delay(300)).subscribe(() => {this.stepper.selectedIndex = +(query.step)})
        this.cd.detectChanges();
      }
    });
  }

  setCompleted(untilNumber: number): void {
    for(let i = 0; i < untilNumber; i++) {
      this.completedEvents[i] = true;
    }
  }

  parseContractor(data) {
    return {
      firstName: data.firstName,
      lastName: data.lastName,
      fatherName: data.fatherName,
      birthDate: data.birthDate,
      bloodTypeId: data.bloodTypeId ? +data.bloodTypeId : null,
      sex: data.sex ? +data.sex : null,
    }
  }
 
  createContractor(): void {
    if (this.queryContractorId) {
      this.buildEmployeRequest(this.queryContractorId).subscribe(() => {
        this.completedEvents[0] = true;
        this.notificationService.success('Success', 'Contractor added!', NotificationUtil.getDefaultMidConfig());
        this.bulletinDataFormComponent.bulletinForm.get('contractorId').patchValue(this.queryContractorId);
        this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
      });
    } else {
      const request = ContractorParser.parseContractor(this.generalDataFormComponent.generalForm.getRawValue());
      this.contractorService.createContractor(request).subscribe(response => {
        const id = +response.data;
        this.contractorId = id;
          this.bulletinDataFormComponent.bulletinForm.get('contractorId').patchValue(id);
          this.completedEvents[0] = true;
          this.notificationService.success('Success', 'Contractor added!', NotificationUtil.getDefaultMidConfig());
      }, error => {
          this.completedEvents[0] = true;
          this.router.navigate(['../'], { relativeTo: this.route });
          this.notificationService.error('Failure', 'Contractor was not added!', NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  createBulletin(): void {
    this.bulletinDataFormComponent.isLoading = true;
    this.buildBulletinDataFormRequest().subscribe(response => {
      if(response)
      {
        this.completedEvents[1] = true;
        this.notificationService.success('Success', 'Bulletin added!', NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
      }

      if(this.queryContractorId)
      {
        this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
      }
    },error => {
        this.completedEvents[1] = true;
        this.notificationService.error('Failure', 'Bulletin was not added!', NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
    } );
  }

  addPicture(): void {
    this.isLoading = true;
    this.buildPhotoRequest(this.contractorId).subscribe(response => {
      if(response)
      {
        this.completedEvents[3] = true;
        this.notificationService.success('Success', 'Picture added!', NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
        this.photoDataFormComponent = true;
      }

      if (this.queryContractorId)
      {
        this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
      }
    }, error =>{
      this.completedEvents[3] = true;
      this.notificationService.error('Failure', 'Picture was not added!', NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    });
  }

  addRequestToEmploy(): void {
    this.requestToEmployDataFormComponent.isLoading = true;
    this.buildEmployeRequest(this.contractorId).subscribe(response => {
      if(response)
      {
        this.completedEvents[4] = true;
        this.notificationService.success('Success', 'Request added!', NotificationUtil.getDefaultMidConfig());
        this.requestToEmployDataFormComponent.isLoading = false;
      }

      if (this.queryContractorId)
      {
        this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
      }
    }, error => {
        this.completedEvents[4] = true;
        this.notificationService.error('Failure', 'Request was not added!', NotificationUtil.getDefaultMidConfig());
        this.requestToEmployDataFormComponent.isLoading = false;
    });
  }

  createStudies(): void {
    this.studiesDataFormComponent.isLoading = true;
    this.buildStudiesForm().subscribe(response => {
      if(response)
      {
        this.completedEvents[3] = true;
        this.notificationService.success('Success', 'Studies added!', NotificationUtil.getDefaultMidConfig());
        this.studiesDataFormComponent.isLoading = false;
      }

      if (this.queryContractorId)
      {
        this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
      }
    }, error => {
      this.completedEvents[3] = true;
        this.notificationService.error('Failure', 'Studies was not added!', NotificationUtil.getDefaultMidConfig());
        this.studiesDataFormComponent.isLoading = false;
    });
  }

  createFiles(): void {
    this.uploadDataFormComponent.isLoading = true;
    forkJoin([...this.buildMultiFileUpload()]).subscribe(response => {
        if(response)
        {
          this.completedEvents[5] = true;
          this.notificationService.success('Success', 'Files added!', NotificationUtil.getDefaultMidConfig());
          this.uploadDataFormComponent.isLoading = false;
          this.router.navigate(['../', this.contractorId, {order: true}], { relativeTo: this.route });
        }
        
        if (this.queryContractorId)
        {
          this.router.navigate(['../../', this.queryContractorId], { relativeTo: this.route });
        }
    },error => {
        this.router.navigate(['../', this.contractorId], { relativeTo: this.route });
        this.uploadDataFormComponent.isLoading = false;
        this.notificationService.error('Failure', 'Files was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  resetForms(): void {
    this.completedEvents.length = 0;
    this.generalDataFormComponent.generalForm.reset();
    this.bulletinDataFormComponent.bulletinForm.reset();
    this.requestToEmployDataFormComponent.requestToEmployForm.reset();
    this.studiesDataFormComponent.studyForm.reset();
  }

  buildFile(data): Observable<any> {
    return this.fileService.create(data);
  }

  buildMultiFileUpload(): Observable<any>[] {
    if (this.uploadDataFormComponent && this.uploadDataFormComponent.files && !this.uploadDataFormComponent.files.length) {
      return [];
    }

    const requests = this.uploadDataFormComponent.files.map(el => this.buildFile(ContractorParser.parseFile(el, this.contractorId, DocumentTypeEnum.Identity)));
    return requests;
  }

  buildStudiesForm(): Observable<any> {
    const request = ContractorParser.parseStudies(this.studiesDataFormComponent.studyForm.getRawValue().studies, this.contractorId);
    return this.studyService.addMultiple(request);
  }

  buildBulletinDataFormRequest(): Observable<any> {
    const request = ContractorParser.parseBulletin(this.bulletinDataFormComponent.bulletinForm.getRawValue());
    return this.bulletinService.add(request);
  }

  buildEmployeRequest(id: number): Observable<any> {
    const request = ContractorParser.parseFile(this.requestToEmployDataFormComponent.requestToEmployForm.get('file').value, id, DocumentTypeEnum.Request);
    return this.fileService.create(request);
  }

  buildPhotoRequest(id: number): Observable<any> {
      const request = new FormData();

    if (this.attachedFile) {
      this.fileType = '7';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
      request.append('ContractorId', this.contractorId);

      return this.contractorService.uploadPhoto(request);
  }

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  get firstForm() {
    return this.generalDataFormComponent && this.generalDataFormComponent.generalForm;
  }

  set firstForm(data) {}

  get secondForm() {
    return this.bulletinDataFormComponent && this.bulletinDataFormComponent.bulletinForm;
  }

  set secondForm(data) {}

  get thirdForm() {
    return this.studiesDataFormComponent && this.studiesDataFormComponent.studyForm;
  }

  set thirdForm(data) {}

  get fourthForm(){
    return this.photoDataFormComponent;
  }

  set fourthForm(data) {}

  get fifthForm(){
    return this.requestToEmployDataFormComponent && this.requestToEmployDataFormComponent.requestToEmployForm;
  }

  set fifthForm(data) {}

}
