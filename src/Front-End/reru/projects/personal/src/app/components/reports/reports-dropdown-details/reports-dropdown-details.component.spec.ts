import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportsDropdownDetailsComponent } from './reports-dropdown-details.component';

describe('ReportsDropdownDetailsComponent', () => {
  let component: ReportsDropdownDetailsComponent;
  let fixture: ComponentFixture<ReportsDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportsDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportsDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
