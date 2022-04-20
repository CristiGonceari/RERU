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
import { UserFilesService } from '../../utils/services/user-files.service';

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
  files: File[] = [];

  userId: any;
  fileId: string;
  fileType: FileTypeEnum = FileTypeEnum.Photos;
  userFileType: FileTypeEnum = FileTypeEnum.Documents;
  attachedFile: File;
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
    private userFilesService: UserFilesService,
    private notificationService: NotificationsService,
    public translate: I18nService,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.currentLanguage = this.translate.currentLanguage;
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
      firstName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      lastName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      fatherName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      idnp: this.fb.control(null, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      email: this.fb.control(null, [Validators.required, Validators.email]),
      emailNotification: this.fb.control(false, [Validators.required]),
    });
  }

  onSelect(event) {
    this.files.push(...event.addedFiles);
  }

  onRemove(event) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
  }

  addUser(): void {
    const request = new FormData();

    let data = {
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      fatherName: this.userForm.value.fatherName,
      email: this.userForm.value.email,
      idnp: this.userForm.value.idnp,
      emailNotification: this.userForm.value.emailNotification = true,
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

      for(let userFile of this.files){
        let formData = new FormData();
        formData.append('Data.UserId', res.data);
        formData.append('Data.File.File', userFile);
        formData.append('Data.File.Type', this.userFileType.toString());
        this.userFilesService.create(formData).subscribe(res => {})
      }

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

  checkFile(event) {
		if (event != null) this.attachedFile = event;
		else this.fileId = null;
	}
}
