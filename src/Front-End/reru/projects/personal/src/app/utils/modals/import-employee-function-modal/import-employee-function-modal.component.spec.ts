import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportEmployeeFunctionModalComponent } from './import-employee-function-modal.component';

describe('ImportEmployeeFunctionModalComponent', () => {
  let component: ImportEmployeeFunctionModalComponent;
  let fixture: ComponentFixture<ImportEmployeeFunctionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportEmployeeFunctionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportEmployeeFunctionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
