import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationsListTableComponent } from './evaluations-list-table.component';

describe('EvaluationsListTableComponent', () => {
  let component: EvaluationsListTableComponent;
  let fixture: ComponentFixture<EvaluationsListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluationsListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluationsListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
