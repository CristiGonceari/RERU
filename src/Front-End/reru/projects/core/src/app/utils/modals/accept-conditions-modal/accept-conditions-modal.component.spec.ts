import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptConditionsModalComponent } from './accept-conditions-modal.component';

describe('AcceptConditionsModalComponent', () => {
  let component: AcceptConditionsModalComponent;
  let fixture: ComponentFixture<AcceptConditionsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcceptConditionsModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptConditionsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
