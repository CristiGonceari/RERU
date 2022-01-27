import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AskGenerateOrderModalComponent } from './ask-generate-order-modal.component';

describe('AskGenerateOrderModalComponent', () => {
  let component: AskGenerateOrderModalComponent;
  let fixture: ComponentFixture<AskGenerateOrderModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AskGenerateOrderModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AskGenerateOrderModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
