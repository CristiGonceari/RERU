import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganigramDropdownDetailsComponent } from './organigram-dropdown-details.component';

describe('OrganigramDropdownDetailsComponent', () => {
  let component: OrganigramDropdownDetailsComponent;
  let fixture: ComponentFixture<OrganigramDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganigramDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganigramDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
