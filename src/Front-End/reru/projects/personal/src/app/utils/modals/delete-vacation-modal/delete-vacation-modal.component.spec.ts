import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteVacationModalComponent } from './delete-vacation-modal.component';

describe('DeleteVacationModalComponent', () => {
  let component: DeleteVacationModalComponent;
  let fixture: ComponentFixture<DeleteVacationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteVacationModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteVacationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
