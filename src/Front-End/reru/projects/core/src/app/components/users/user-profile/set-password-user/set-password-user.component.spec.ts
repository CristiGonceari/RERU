/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SetPasswordUserComponent } from './set-password-user.component';

describe('SetPasswordUserComponent', () => {
  let component: SetPasswordUserComponent;
  let fixture: ComponentFixture<SetPasswordUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetPasswordUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetPasswordUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
