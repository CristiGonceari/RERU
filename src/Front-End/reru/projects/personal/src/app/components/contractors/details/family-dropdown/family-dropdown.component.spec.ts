import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FamilyDropdownComponent } from './family-dropdown.component';

describe('FamilyDropdownComponent', () => {
  let component: FamilyDropdownComponent;
  let fixture: ComponentFixture<FamilyDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FamilyDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FamilyDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
