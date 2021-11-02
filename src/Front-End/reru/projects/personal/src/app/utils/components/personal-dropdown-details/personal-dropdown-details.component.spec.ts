import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalDropdownDetailsComponent } from './personal-dropdown-details.component';

describe('PersonalDropdownDetailsComponent', () => {
  let component: PersonalDropdownDetailsComponent;
  let fixture: ComponentFixture<PersonalDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonalDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
