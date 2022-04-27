import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSolicitedTestsTableComponent } from './user-solicited-tests-table.component';

describe('UserSolicitedTestsTableComponent', () => {
  let component: UserSolicitedTestsTableComponent;
  let fixture: ComponentFixture<UserSolicitedTestsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserSolicitedTestsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSolicitedTestsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
