import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEvaluatedTestsListComponent } from './user-evaluated-tests-list.component';

describe('UserEvaluatedTestsListComponent', () => {
  let component: UserEvaluatedTestsListComponent;
  let fixture: ComponentFixture<UserEvaluatedTestsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserEvaluatedTestsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserEvaluatedTestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
