import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteInstructionModalComponent } from './delete-instruction-modal.component';

describe('DeleteInstructionModalComponent', () => {
  let component: DeleteInstructionModalComponent;
  let fixture: ComponentFixture<DeleteInstructionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteInstructionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteInstructionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
