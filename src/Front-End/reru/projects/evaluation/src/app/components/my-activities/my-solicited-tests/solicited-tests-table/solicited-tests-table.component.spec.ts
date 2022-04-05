import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitedTestsTableComponent } from './solicited-tests-table.component';

describe('SolicitedTestsTableComponent', () => {
  let component: SolicitedTestsTableComponent;
  let fixture: ComponentFixture<SolicitedTestsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitedTestsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitedTestsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
