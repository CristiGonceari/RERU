import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationDeleteContractorComponent } from './confirmation-delete-contractor.component';

describe('ConfirmationDeleteContractorComponent', () => {
  let component: ConfirmationDeleteContractorComponent;
  let fixture: ComponentFixture<ConfirmationDeleteContractorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmationDeleteContractorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmationDeleteContractorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
