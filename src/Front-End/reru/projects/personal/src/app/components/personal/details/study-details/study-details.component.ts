import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteStudyModalComponent } from 'projects/personal/src/app/utils/modals/delete-study-modal/delete-study-modal.component';
import { EditStydyModalComponent } from 'projects/personal/src/app/utils/modals/edit-stydy-modal/edit-stydy-modal.component';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { StudyModel } from 'projects/personal/src/app/utils/models/study.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { StudyService } from 'projects/personal/src/app/utils/services/study.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ContractorParser } from '../../add/add.parser';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-study-details',
  templateUrl: './study-details.component.html',
  styleUrls: ['./study-details.component.scss']
})
export class StudyDetailsComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractor: Contractor;
  isLoading: boolean = true;
  studyForm: FormGroup;
  studies: StudyModel[];
  studyTypes: SelectItem[];

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{label:"", value:""}];

  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }
  constructor(private fb: FormBuilder,
              private studyService: StudyService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private referenceService: ReferenceService) { }

  ngOnInit(): void {
    this.retrieveDropdowns();
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 5 }).subscribe(response => {
      this.studyTypes = response.data;

      this.retrieveStudies();

      this.isLoading = false;
    });
  }

  retrieveStudies(data: any = {}): void {
    const request = {
      contractorId: this.contractor.id,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }
    this.studyService.get(request).subscribe((response: ApiResponse<any>) => {
      this.studies = [...response.data.items];
      this.pagedSummary = response.data.pagedSummary;
      this.initForm(response.data.items);
    });
  }

  initForm(studies: StudyModel[]): void {
    studies.forEach((element, index) => {
      this.selectedItems[index] = this.studyTypes && this.studyTypes.find(el => +el.value === element.studyTypeId);
    });

    this.studyForm = this.fb.group({
      studies: this.fb.array(this.buildStudies(studies))
    });
    this.isLoading = false;
  }

  buildStudies(studies): StudyModel[] {
    return studies.map((el: StudyModel) => this.generateStudy(el));
  }

  searchStudyType(index, text$) {
    return (text$: Observable<string>) => {
      const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
      const clicksWithClosedPopup$ = this.click$[index].pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
      const inputFocus$ = this.focus$[index];

      return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
        map(term => (term === '' ? this.studyTypes
          : this.studyTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
    }
  }

  selectStudyType(studyType: SelectItem, index:number): void {
    (<FormArray>this.studyForm.controls.studies).controls[index].get('studyTypeId').patchValue(studyType ? +studyType.value : null);
  }

  generateStudy(study: StudyModel): FormGroup {
    return this.fb.group({
      id: this.fb.control(study.id),
      institution: this.fb.control({ value: study.institution, disabled: true }, [Validators.required]),
      studyFrequency: this.fb.control({ value: study.studyFrequency, disabled: true }, [Validators.required]),
      faculty: this.fb.control({ value: study.faculty, disabled: true }, [Validators.required]),
      qualification: this.fb.control({ value: study.qualification, disabled: true }, [Validators.required]),
      specialty: this.fb.control({ value: study.specialty, disabled: true }, [Validators.required]),
      diplomaNumber: this.fb.control({ value: study.diplomaNumber, disabled: true }, [Validators.required]),
      diplomaReleaseDay: this.fb.control({ value: study.diplomaReleaseDay, disabled: true }, [Validators.required]),
      isActiveStudy: this.fb.control({ value: study.isActiveStudy, disabled: true }, [Validators.required]),
      contractorId: this.fb.control({ value: study.contractorId, disabled: true }, []),
      studyTypeId: this.fb.control({ value: study.studyTypeId, disabled: true }, [])
    });
  }

  openEditStudyModal(study: FormGroup, index: number): void {
    const modalRef = this.modalService.open(EditStydyModalComponent, { centered: true, backdrop: 'static', size: 'lg'});
    modalRef.componentInstance.study = {...study.getRawValue()};
    modalRef.result.then((response) => this.editStudy(response), () => {});
  }

  editStudy(data: StudyModel): void {
    this.isLoading = true;
    this.studyService.update(data).subscribe(response => {
      this.retrieveStudies();
      this.notificationService.success('Success', 'Study updated!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
    });
  }

  openDeleteStudyModal(id: number): void {
    const modalRef = this.modalService.open(DeleteStudyModalComponent, { centered: true, backdrop: 'static'});
    modalRef.result.then(() => this.deleteStudy(id), () => {});
  }

  deleteStudy(id: number): void {
    this.isLoading = true;
    this.studyService.delete(id).subscribe(response => {
      this.retrieveStudies();
      this.notificationService.success('Success', 'Study deleted!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
    });
  }

  openAddStudyModal(): void {
    const modalRef = this.modalService.open(EditStydyModalComponent, { centered: true, backdrop: 'static', size: 'lg'});
    modalRef.componentInstance.study = <StudyModel>{ contractorId: this.contractor.id };
    modalRef.componentInstance.study.studyFrequency = 0;
    modalRef.result.then((response) => this.addStudy(response), () => {});
  }

  addStudy(data: StudyModel): void {
    this.isLoading = true;
    this.studyService.add(ContractorParser.parseStudy(data, this.contractor.id)).subscribe(response => {
      this.retrieveStudies();
      this.notificationService.success('Success', 'Study added!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
    });
  }

  formatter = (x:SelectItem) => x.label;
}
