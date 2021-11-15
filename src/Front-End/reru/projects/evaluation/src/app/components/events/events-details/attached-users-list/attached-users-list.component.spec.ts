import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachedUsersListComponent } from './attached-users-list.component';

describe('AttachedUsersListComponent', () => {
  let component: AttachedUsersListComponent;
  let fixture: ComponentFixture<AttachedUsersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttachedUsersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachedUsersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
