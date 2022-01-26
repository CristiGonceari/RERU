import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteHolidayModalComponent } from './delete-holiday-modal.component';

describe('DeleteHolidayModalComponent', () => {
  let component: DeleteHolidayModalComponent;
  let fixture: ComponentFixture<DeleteHolidayModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteHolidayModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteHolidayModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
