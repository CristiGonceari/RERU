import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualContractRequestComponent } from './individual-contract-request.component';

describe('IndividualContractRequestComponent', () => {
  let component: IndividualContractRequestComponent;
  let fixture: ComponentFixture<IndividualContractRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualContractRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndividualContractRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
