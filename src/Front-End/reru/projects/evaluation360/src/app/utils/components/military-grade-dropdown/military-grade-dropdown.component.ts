import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin, merge, Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { SelectItem } from '../../models/select-item.model';
import { I18nService } from '../../services/i18n.service';
import { isInvalidPattern, isInvalidRequired, isValid } from '../../util/forms.util';

@Component({
  selector: 'military-grade-dropdown',
  templateUrl: './military-grade-dropdown.component.html',
  styleUrls: ['./military-grade-dropdown.component.scss']
})
export class MilitaryGradeDropdownComponent implements OnInit {
  @ViewChild('instance') typeAheadInstance: NgbTypeahead;
  @Input() formGroup: FormGroup;
  isDisabled: boolean;
  selectedMilitaryGrade: SelectItem;
  militaryGradesTranslation: SelectItem[] = [];
  militaryGradesTitles: {
    militaryGrades: string;
    specialGrades: string;
    specialInternGrades: string;
  }
  isInvalidPattern: Function;
  isInvalidRequired: Function;
  isValid: Function;

  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  constructor(private readonly translateService: I18nService) {
    this.isValid = isValid.bind(this);
    this.isInvalidPattern = isInvalidPattern.bind(this);
    this.isInvalidRequired = isInvalidRequired.bind(this);
   }

  ngOnInit(): void {
    this.initTranslation();
    this.translateService.change.subscribe(() => this.initTranslation());
    this.initDropdown();
  }

  initDropdown(): void {
    const value = this.formGroup?.get('specialOrMilitaryGrade')?.value;
    this.isDisabled = this.formGroup?.get('specialOrMilitaryGrade')?.disabled;
    if (value) {
      const foundMilitaryGrade = this.militaryGradesTranslation.find(el => +el.value === +value);
      if (foundMilitaryGrade) {
        this.selectedMilitaryGrade = foundMilitaryGrade;
        this.formGroup.get('specialOrMilitaryGrade').markAsTouched();
      }
    }
  }

  initTranslation(): void {
    const promises = [];
    this.militaryGradesTranslation.length = 0;
    for(let i = 0; i < 45; i++) {
      promises.push(this.translateService.get(`evaluations.military-grade.${i+1}`))
    }

    promises.push(
      this.translateService.get('evaluations.military-grade.military-grades-title'),
      this.translateService.get('evaluations.military-grade.special-grades-title'),
      this.translateService.get('evaluations.military-grade.special-intern-grades-title')
    )

    forkJoin(promises).subscribe((grades: any[]) => {
      for(let i = 0; i < 45; i++) {
        this.militaryGradesTranslation.push({ value: (i+1)+'', label: grades[i] })
      }

      this.militaryGradesTitles = {
        militaryGrades: grades[45],
        specialGrades: grades[46],
        specialInternGrades: grades[47]
      }
    });
  }

  setMilitaryGrade(event: { item: SelectItem }): void {
    if (!isNaN(+event.item.value)) {
      this.setFormControl(+event.item.value);
    } else {
      this.setFormControl();
    }
  }

  search = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => !this.typeAheadInstance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map((term: string) => {
        switch(true) {
          case term === '': this.formGroup.get('specialOrMilitaryGrade').patchValue(null);
                            return this.militaryGradesTranslation;
          case this.militaryGradesTitles.militaryGrades.toLowerCase().indexOf(term.toLowerCase()) > -1:
                            return this.militaryGradesTranslation.filter((_, i) => i < 15);
          case this.militaryGradesTitles.specialGrades.toLowerCase().indexOf(term.toLowerCase()) > -1:
                            return this.militaryGradesTranslation.filter((_, i) => i > 14 && i < 30);
          case this.militaryGradesTitles.specialInternGrades.toLowerCase().indexOf(term.toLowerCase()) > -1:
                            return this.militaryGradesTranslation.filter((_, i) => i > 29);
          default:
            if (!this.militaryGradesTranslation.some(el => el.label === term)) {
              this.formGroup.get('specialOrMilitaryGrade').patchValue(null);
            }
            return this.militaryGradesTranslation.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)?.slice(0, 10);
        }  
      })
    )
  }

  formatter = (x: SelectItem) => x.label;

  private setFormControl(value = null): void {
    this.formGroup.get('specialOrMilitaryGrade').patchValue(value);
    this.formGroup.get('specialOrMilitaryGrade').markAsTouched();
  }
}
