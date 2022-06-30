import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserReceivedEvaluationsComponent } from './user-received-evaluations.component';

describe('UserReceivedEvaluationsComponent', () => {
  let component: UserReceivedEvaluationsComponent;
  let fixture: ComponentFixture<UserReceivedEvaluationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserReceivedEvaluationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserReceivedEvaluationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
