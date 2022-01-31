import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEvaluatedTestsComponent } from './user-evaluated-tests.component';

describe('UserEvaluatedTestsComponent', () => {
  let component: UserEvaluatedTestsComponent;
  let fixture: ComponentFixture<UserEvaluatedTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserEvaluatedTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserEvaluatedTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
