import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentPositionTableComponent } from './department-position-table.component';

describe('DepartmentPositionTableComponent', () => {
  let component: DepartmentPositionTableComponent;
  let fixture: ComponentFixture<DepartmentPositionTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentPositionTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentPositionTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
