import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterByDepartmentsComponent } from './filter-by-departments.component';

describe('FilterByDepartmentsComponent', () => {
  let component: FilterByDepartmentsComponent;
  let fixture: ComponentFixture<FilterByDepartmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterByDepartmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterByDepartmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
