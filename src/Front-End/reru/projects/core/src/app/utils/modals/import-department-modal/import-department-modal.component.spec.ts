import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportDepartmentModalComponent } from './import-department-modal.component';

describe('ImportDepartmentModalComponent', () => {
  let component: ImportDepartmentModalComponent;
  let fixture: ComponentFixture<ImportDepartmentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportDepartmentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportDepartmentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
