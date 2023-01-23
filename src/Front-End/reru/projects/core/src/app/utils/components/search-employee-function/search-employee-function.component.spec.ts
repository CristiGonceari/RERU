import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchEmployeeFunctionComponent } from './search-employee-function.component';

describe('SearchEmployeeFunctionComponent', () => {
  let component: SearchEmployeeFunctionComponent;
  let fixture: ComponentFixture<SearchEmployeeFunctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchEmployeeFunctionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchEmployeeFunctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
