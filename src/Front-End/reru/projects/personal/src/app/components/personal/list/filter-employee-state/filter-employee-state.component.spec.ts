import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterEmployeeStateComponent } from './filter-employee-state.component';

describe('FilterEmployeeStateComponent', () => {
  let component: FilterEmployeeStateComponent;
  let fixture: ComponentFixture<FilterEmployeeStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterEmployeeStateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterEmployeeStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
