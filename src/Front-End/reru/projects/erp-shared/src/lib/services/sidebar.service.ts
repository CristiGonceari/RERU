import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SidebarView } from '../models/sidebar.model';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private readonly isEnclosedKey = 'erpIsEnclosed';
  private readonly sidebarSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private readonly modulesSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private readonly userSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private readonly enclosedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public readonly sidebar$ = this.sidebarSubject.asObservable();
  public readonly modules$ = this.modulesSubject.asObservable();
  public readonly user$ = this.userSubject.asObservable();
  public readonly enclosed$ = this.enclosedSubject.asObservable();
  constructor() { 
    this.initializeInterfaceTogglers();
  }

  private initializeInterfaceTogglers(): void {
    const isEnclosed = localStorage.getItem(this.isEnclosedKey);
    if (isEnclosed) this.enclosedSubject.next(isEnclosed === 'true' ? true : false);
  }

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

  toggleIsEnclosed(): void {
    localStorage.setItem(this.isEnclosedKey, (!this.enclosedSubject.getValue())+'')
    this.enclosedSubject.next(!this.enclosedSubject.getValue());
  }

  isEnclosedOn(): boolean {
    return this.enclosedSubject.getValue();
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
