import { Pipe, PipeTransform } from '@angular/core';
import { SelectItem } from '../models/select-item.model';

interface IMilitaryGradePipe {
  militaryGrades: string
  specialGrades: string;
  specialInternGrades: string;
}

@Pipe({
  name: 'militaryGrade'
})
export class MilitaryGradePipe implements PipeTransform {

  transform(value: SelectItem, militaryGradesTranslation: SelectItem[], militaryGradesTitles: IMilitaryGradePipe): string {
    const { militaryGrades, specialGrades, specialInternGrades } = militaryGradesTitles;
    const index = militaryGradesTranslation.indexOf(value);
    if (index < 15) {
      return militaryGrades;
    }

    if (index > 14 && index < 30) {
      return specialGrades;
    }

    if (index > 29) {
      return specialInternGrades;
    }

    return '-';
  }

}
