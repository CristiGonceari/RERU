import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationContactDeleteModalComponent } from './confirmation-contact-delete-modal.component';

describe('ConfirmationContactDeleteModalComponent', () => {
  let component: ConfirmationContactDeleteModalComponent;
  let fixture: ComponentFixture<ConfirmationContactDeleteModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmationContactDeleteModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmationContactDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
