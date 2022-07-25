import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportDepartmentsModalComponent } from './import-departments-modal.component';

describe('ImportDepartmentsModalComponent', () => {
  let component: ImportDepartmentsModalComponent;
  let fixture: ComponentFixture<ImportDepartmentsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportDepartmentsModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportDepartmentsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
