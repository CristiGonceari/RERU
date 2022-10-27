import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchDepartmentComponent } from './search-department.component';

describe('SearchDepartmentComponent', () => {
  let component: SearchDepartmentComponent;
  let fixture: ComponentFixture<SearchDepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchDepartmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
