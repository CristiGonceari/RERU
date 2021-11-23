import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditModuleAccessComponent } from './add-edit-module-access.component';

describe('AddEditModuleAccessComponent', () => {
  let component: AddEditModuleAccessComponent;
  let fixture: ComponentFixture<AddEditModuleAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditModuleAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditModuleAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
