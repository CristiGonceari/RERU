import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AddEditModuleComponent } from './add-edit-module.component';

describe('AddModuleComponent', () => {
  let component: AddEditModuleComponent;
  let fixture: ComponentFixture<AddEditModuleComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEditModuleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
