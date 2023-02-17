import { UserService } from './../../../utils/services/user.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ValidatorUtil } from '../../../utils/util/validator.util';
import { FileTypeEnum } from '@erp/shared';
import { DepartmentService } from '../../../utils/services/department.service';
import { UserRoleService } from '../../../utils/services/user-role.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { SelectItem } from '../../../utils/models/select-item.model';
import { AccessModeEnum } from '../../../utils/models/access-mode.enum';
import { ReferenceService } from '../../../utils/services/reference.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  userForm: FormGroup;
  title: string;
  description: string;

	isLoading: boolean;
  userId: any;
  fileId: string;
  fileType: FileTypeEnum = FileTypeEnum.Photos
  attachedFile: File;
  departments: SelectItem[] = [{ label: '', value: '' }];
  roles: SelectItem[] = [{ label: '', value: '' }];
  accessModes: SelectItem[] = [{ label: '', value: '' }];
  employeeFunctions: SelectItem[] = [{ label: '', value: '' }];
  accesModeEnum = AccessModeEnum;

  birthDate: Date;
  date: string;
  startDate: string;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private userProfileService: UserProfileService,
    private notificationService: NotificationsService,
    private router: Router,
    public translate: I18nService,
    private ngZone: NgZone,
    private route: ActivatedRoute,
    private location: Location,
    private departmentService: DepartmentService,
    private roleService: UserRoleService,
    private referenceService: ReferenceService,
  ) { }

  ngOnInit(): void {
    this.getAccessMode();
    this.getDepartments();
    this.getRoles();
    this.getEmployeeFunctions();
    this.initForm();
    if (this.userId) this.get();
  }

  getDepartments(){
    this.departmentService.getValues().subscribe(res => this.departments = res.data);
  }

  getRoles(){
    this.roleService.getValues().subscribe(res => this.roles = res.data);
  }

  getEmployeeFunctions(){
    this.referenceService.getEmployeeFunctionsSelectValues().subscribe(res => this.employeeFunctions = res.data);
  }

  getAccessMode(){
    this.userProfileService.getAccessMode().subscribe(res => this.accessModes = res.data);
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
  get() {
    this.userService.getUser(this.userId).subscribe(res => {
      if (res && res.data) {
        this.fileId = res.data.mediaFileId;
      }
    });
  }

  initForm(): void {
    this.userForm = this.fb.group({
      firstName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$'),]),
      lastName: this.fb.control(null, [Validators.required, Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$|^(?!À-Ö)[A-Za-z0-9\',\-ĂăÎîȘșȚțÂâ ]*$'),]),
      fatherName: this.fb.control(""),
      idnp: this.fb.control(null, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      email: this.fb.control(null, [Validators.required, Validators.email]),
      phoneNumber: this.fb.control("+373", [Validators.required, Validators.pattern("^((\\+373-?)|0)?[0-9]{8}$"), Validators.maxLength(12), Validators.minLength(12)]),
      departmentColaboratorId: this.fb.control(null, [Validators.required]),
      roleColaboratorId: this.fb.control(null, [Validators.required]),
      functionColaboratorId: this.fb.control(null, [Validators.required]),
      emailNotification: this.fb.control(false, [Validators.required]),
      accessModeEnum: this.fb.control(null, [Validators.required])
    });
  }

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.userForm, field);
  }

  setBirthDate(): void {
		if (this.birthDate) {
			const date = new Date(this.birthDate);
			this.date = new Date(date.getTime() - (new Date(this.birthDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

  addUser(): void {
    this.setBirthDate();
    let data = {
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      fatherName: this.userForm.value.fatherName,
      email: this.userForm.value.email,
      idnp: this.userForm.value.idnp,
      departmentColaboratorId: this.userForm.value.departmentColaboratorId,
      roleColaboratorId: this.userForm.value.roleColaboratorId,
      functionColaboratorId: this.userForm.value.functionColaboratorId,
      emailNotification: this.userForm.value.emailNotification,
      birthDate: this.date || null,
      phoneNumber: this.userForm.value.phoneNumber,
      accessModeEnum: this.userForm.value.accessModeEnum
    }

    this.userService.createUser(data).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('user.succes-create'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });

      const request = new FormData();
      if (this.attachedFile) {
        request.append('Data.File.File', this.attachedFile);
        request.append('Data.File.Type', this.fileType.toString());
      }
      request.append('Data.UserId', res.data)

      this.userService.addUserAvatar(request).subscribe(() => {
        this.isLoading = false;
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
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

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  back(): void {
    this.location.back();
  }
}
