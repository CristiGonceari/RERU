import { Pipe, PipeTransform } from '@angular/core';
import { ObjectUtil } from '../util/object.util'; 

@Pipe({
  name: 'viewDate'
})
export class ViewDatePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    const isDate = ObjectUtil.hasMatchEvent(value) && value.match(new RegExp(/(^\d{2}$)|(^\d{2}\.\d{2}$)/g));
    const isDateStruct = value && (value as any).year && (value as any).month && (value as any).day;

    if (isDate && !isDateStruct) {
      return `${value}.`
    }

    return value;
  }
}
