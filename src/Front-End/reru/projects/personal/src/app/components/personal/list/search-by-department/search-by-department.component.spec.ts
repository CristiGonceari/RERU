import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByDepartmentComponent } from './search-by-department.component';

describe('SearchByDepartmentComponent', () => {
  let component: SearchByDepartmentComponent;
  let fixture: ComponentFixture<SearchByDepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByDepartmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
