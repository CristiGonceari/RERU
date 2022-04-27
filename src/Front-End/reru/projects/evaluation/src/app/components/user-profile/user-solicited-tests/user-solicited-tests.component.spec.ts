import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSolicitedTestsComponent } from './user-solicited-tests.component';

describe('UserSolicitedTestsComponent', () => {
  let component: UserSolicitedTestsComponent;
  let fixture: ComponentFixture<UserSolicitedTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserSolicitedTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSolicitedTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
