import { Component, EventEmitter, NgZone, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { FileTypeEnum } from 'projects/erp-shared/src/lib/models/FileTypeEnum';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../utils/services/i18n.service';
import { UserService } from '../../utils/services/user.service';
import { InregistrationUserService } from '../../utils/services/inregistration-user.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { ValidatorUtil } from '../../utils/util/validator.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { WatchInfoVideoModalComponent } from '../../utils/modals/watch-info-video-modal/watch-info-video-modal.component'
import { CandidatePositionService } from '../../utils/services/candidate-position.service';
import { CandidatePositionModel } from '../../utils/models/candidate-position.model';
import { SelectItem } from '../../utils/models/select-item.model';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss']
})
export class RegistrationPageComponent implements OnInit {

  userForm: FormGroup;
  title: string;
  description: string;
  success: boolean = false;

  userId: any;
  fileId: string;
  fileType: FileTypeEnum = FileTypeEnum.Photos;
  attachedFile: File;
  candidatePositions: SelectItem[] = [{ label: '', value: '' }];
  selectedProject: number;

  languageList = [
    { code: 'en', label: 'English' },
    { code: 'ro', label: 'Română' },
    { code: 'ru', label: 'Русский' },
  ];
  currentLanguage: string;

  isCollapsed = true;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private inregistrationService: InregistrationUserService,
    private notificationService: NotificationsService,
    private candidatePosition: CandidatePositionService,
    public translate: I18nService,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.retrievePositions();
    this.currentLanguage = this.translate.currentLanguage;
  }

  retrievePositions(){
    this.candidatePosition.getPositionValues().subscribe((res) => (
        this.candidatePositions = res.data
      ));
  }

  atachProject(item: any) {
    this.selectedProject = item.target.value;
  }

  openModal(){
    const modalRef = this.modalService.open( WatchInfoVideoModalComponent, { centered: true, size: 'lg', windowClass: 'my-class' });
  }

  getLang(): string {
    const value = this.languageList.find(l => l.code == this.currentLanguage);

    return (value.label) || "Language";
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }

  hasErrors(field): boolean {
    return this.userForm.touched && this.userForm.get(field).invalid;
  }

  hasError(field: string, error = 'required'): boolean {
    return (
      this.userForm.get(field).invalid &&
      this.userForm.get(field).touched &&
      this.userForm.get(field).hasError(error)
    );
  }

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.userForm, field);
  }

  initForm(): void {
    this.userForm = this.fb.group({
      name: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      lastName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      fatherName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      idnp: this.fb.control(null, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      email: this.fb.control(null, [Validators.required, Validators.email]),
      emailNotification: this.fb.control(false, [Validators.required]),
      candidatePositionId: this.fb.control((this.selectedProject), [Validators.required]),
    });
  }

  addUser(): void {
    const request = new FormData();

    let data = {
      name: this.userForm.value.name,
      lastName: this.userForm.value.lastName,
      fatherName: this.userForm.value.fatherName,
      email: this.userForm.value.email,
      idnp: this.userForm.value.idnp,
      emailNotification: this.userForm.value.emailNotification = true,
      candidatePositionId: +this.selectedProject
    }

    this.inregistrationService.inregistrateUser(data).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('user.succes-create'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        this.success = true;
      });

      if (this.attachedFile) {
        request.append('Data.File.File', this.attachedFile);
        request.append('Data.File.Type', this.fileType.toString());
      }
      request.append('Data.UserId', res.data);

      this.userService.addUserAvatar(request).subscribe(() => {
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      })

    }, () => {
      forkJoin([
        this.translate.get('notification.title.error'),
        this.translate.get('notification.body.error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }
}
