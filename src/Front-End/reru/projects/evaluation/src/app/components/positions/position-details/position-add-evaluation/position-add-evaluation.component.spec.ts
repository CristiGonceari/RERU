import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionAddEvaluationComponent } from './position-add-evaluation.component';

describe('PositionAddEvaluationComponent', () => {
  let component: PositionAddEvaluationComponent;
  let fixture: ComponentFixture<PositionAddEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionAddEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionAddEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
