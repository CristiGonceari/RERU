import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FaqDropdownDetailsComponent } from './faq-dropdown-details.component';

describe('FaqDropdownDetailsComponent', () => {
  let component: FaqDropdownDetailsComponent;
  let fixture: ComponentFixture<FaqDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FaqDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FaqDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
