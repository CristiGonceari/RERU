import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVacationModalComponent } from './add-vacation-modal.component';

describe('AddVacationModalComponent', () => {
  let component: AddVacationModalComponent;
  let fixture: ComponentFixture<AddVacationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddVacationModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddVacationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
