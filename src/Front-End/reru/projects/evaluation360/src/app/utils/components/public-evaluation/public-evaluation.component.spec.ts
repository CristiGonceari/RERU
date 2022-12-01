import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicEvaluationComponent } from './public-evaluation.component';

describe('PublicEvaluationComponent', () => {
  let component: PublicEvaluationComponent;
  let fixture: ComponentFixture<PublicEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
