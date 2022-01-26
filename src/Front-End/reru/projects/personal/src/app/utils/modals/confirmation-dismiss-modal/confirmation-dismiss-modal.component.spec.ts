import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationDismissModalComponent } from './confirmation-dismiss-modal.component';

describe('ConfirmationDismissModalComponent', () => {
  let component: ConfirmationDismissModalComponent;
  let fixture: ComponentFixture<ConfirmationDismissModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmationDismissModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmationDismissModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
