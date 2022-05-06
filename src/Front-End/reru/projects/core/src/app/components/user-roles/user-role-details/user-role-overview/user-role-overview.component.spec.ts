import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRoleOverviewComponent } from './user-role-overview.component';

describe('UserRoleOverviewComponent', () => {
  let component: UserRoleOverviewComponent;
  let fixture: ComponentFixture<UserRoleOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserRoleOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserRoleOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
