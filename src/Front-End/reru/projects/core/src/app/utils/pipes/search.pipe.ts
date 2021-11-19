import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(value: any, ...args: any[]): unknown {
    if (!value || !value.length) {
      return [];
    }

    if (!args[1]) {
      return value;
    }

    return value.filter(el => el[args[0]] === args[1]);
  }

}
