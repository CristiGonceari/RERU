import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentDropdownDetailsComponent } from './department-dropdown-details.component';

describe('DropdownDetailsComponent', () => {
  let component: DepartmentDropdownDetailsComponent;
  let fixture: ComponentFixture<DepartmentDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
