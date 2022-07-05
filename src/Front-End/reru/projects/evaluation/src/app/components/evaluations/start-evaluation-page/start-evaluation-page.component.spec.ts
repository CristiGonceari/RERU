import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartEvaluationPageComponent } from './start-evaluation-page.component';

describe('StartEvaluationPageComponent', () => {
  let component: StartEvaluationPageComponent;
  let fixture: ComponentFixture<StartEvaluationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartEvaluationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartEvaluationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
