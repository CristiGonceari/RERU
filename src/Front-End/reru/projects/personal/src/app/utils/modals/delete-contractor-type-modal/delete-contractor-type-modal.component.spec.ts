import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteContractorTypeModalComponent } from './delete-contractor-type-modal.component';

describe('DeleteContractorTypeModalComponent', () => {
  let component: DeleteContractorTypeModalComponent;
  let fixture: ComponentFixture<DeleteContractorTypeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteContractorTypeModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteContractorTypeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
