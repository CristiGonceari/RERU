import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagesDropdownDetailsComponent } from './languages-dropdown-details.component';

describe('LanguagesDropdownDetailsComponent', () => {
  let component: LanguagesDropdownDetailsComponent;
  let fixture: ComponentFixture<LanguagesDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguagesDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagesDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
