import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOldPositionModalComponent } from './add-old-position-modal.component';

describe('AddOldPositionModalComponent', () => {
  let component: AddOldPositionModalComponent;
  let fixture: ComponentFixture<AddOldPositionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOldPositionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOldPositionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
