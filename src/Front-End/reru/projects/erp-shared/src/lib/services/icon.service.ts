import { Injectable } from '@angular/core';
import * as data from '../assets/icons.json';
import { IconModel } from '../models/icon.model';

@Injectable({
  providedIn: 'root'
})
export class IconService {
  icons: any = (data as any).default;

  constructor() { }

  list(): IconModel[] {
    return this.icons;
  }
}
