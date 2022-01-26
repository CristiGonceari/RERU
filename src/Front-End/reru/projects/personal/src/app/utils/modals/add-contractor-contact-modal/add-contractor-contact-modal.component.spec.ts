import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddContractorContactModalComponent } from './add-contractor-contact-modal.component';

describe('AddContractorContactModalComponent', () => {
  let component: AddContractorContactModalComponent;
  let fixture: ComponentFixture<AddContractorContactModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddContractorContactModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddContractorContactModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
