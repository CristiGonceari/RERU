import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeCurrentPositionModalComponent } from './change-current-position-modal.component';

describe('ChangeCurrentPositionModalComponent', () => {
  let component: ChangeCurrentPositionModalComponent;
  let fixture: ComponentFixture<ChangeCurrentPositionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeCurrentPositionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeCurrentPositionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
