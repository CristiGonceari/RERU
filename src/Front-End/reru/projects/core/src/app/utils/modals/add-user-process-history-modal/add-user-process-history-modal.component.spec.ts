import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserProcessHistoryModalComponent } from './add-user-process-history-modal.component';

describe('AddUserProcessHistoryModalComponent', () => {
  let component: AddUserProcessHistoryModalComponent;
  let fixture: ComponentFixture<AddUserProcessHistoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUserProcessHistoryModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUserProcessHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
