import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferNewPositionModalComponent } from './transfer-new-position-modal.component';

describe('TransferNewPositionModalComponent', () => {
  let component: TransferNewPositionModalComponent;
  let fixture: ComponentFixture<TransferNewPositionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransferNewPositionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransferNewPositionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
