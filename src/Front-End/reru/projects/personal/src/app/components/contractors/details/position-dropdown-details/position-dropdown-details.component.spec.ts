import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionDropdownDetailsComponent } from './position-dropdown-details.component';

describe('PositionDropdownDetailsComponent', () => {
  let component: PositionDropdownDetailsComponent;
  let fixture: ComponentFixture<PositionDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
