import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SidebarView } from '../models/sidebar.model';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private sidebarSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private modulesSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private userSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public sidebar$ = this.sidebarSubject.asObservable();
  public modules$ = this.modulesSubject.asObservable();
  public user$ = this.userSubject.asObservable();
  constructor() { }

  toggle(view: SidebarView, closeAll: boolean = false): void {
    if (closeAll) {
      this.closeAll(view);
    }

    switch (view) {
      case SidebarView.SIDEBAR:
        this.sidebarSubject.next(!this.sidebarSubject.getValue());
        break;
      case SidebarView.MODULES:
        this.modulesSubject.next(!this.modulesSubject.getValue());
        break;
      case SidebarView.USER:
        this.userSubject.next(!this.userSubject.getValue());
        break;
    }
  }

  isSidebarOn(): boolean {
    return this.sidebarSubject.getValue();
  }

  closeAll(except: SidebarView): void {
    Object.keys(SidebarView).map(el => {
      const key = el.toUpperCase();

      if (except !== SidebarView[key]) {
        this[`${key}Subject`].next(false);
      }
    });
  }
}
