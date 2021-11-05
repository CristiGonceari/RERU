import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastDepartmentLabelComponent } from './last-department-label.component';

describe('LastDepartmentLabelComponent', () => {
  let component: LastDepartmentLabelComponent;
  let fixture: ComponentFixture<LastDepartmentLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastDepartmentLabelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastDepartmentLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
