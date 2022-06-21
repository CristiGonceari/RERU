import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyEmailCodeModalComponent } from './verify-email-code-modal.component';

describe('VerifyEmailCodeModalComponent', () => {
  let component: VerifyEmailCodeModalComponent;
  let fixture: ComponentFixture<VerifyEmailCodeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VerifyEmailCodeModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyEmailCodeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
