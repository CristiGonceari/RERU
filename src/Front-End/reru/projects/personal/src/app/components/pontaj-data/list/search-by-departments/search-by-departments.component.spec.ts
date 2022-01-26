import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByDepartmentsComponent } from './search-by-departments.component';

describe('SearchByDepartmentsComponent', () => {
  let component: SearchByDepartmentsComponent;
  let fixture: ComponentFixture<SearchByDepartmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByDepartmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByDepartmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
