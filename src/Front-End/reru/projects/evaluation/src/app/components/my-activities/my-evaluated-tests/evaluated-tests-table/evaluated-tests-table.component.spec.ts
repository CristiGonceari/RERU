import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluatedTestsTableComponent } from './evaluated-tests-table.component';

describe('EvaluatedTestsTableComponent', () => {
  let component: EvaluatedTestsTableComponent;
  let fixture: ComponentFixture<EvaluatedTestsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluatedTestsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluatedTestsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
