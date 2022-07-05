import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewUsersModalComponent } from './view-users-modal.component';

describe('ViewUsersModalComponent', () => {
  let component: ViewUsersModalComponent;
  let fixture: ComponentFixture<ViewUsersModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewUsersModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewUsersModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
