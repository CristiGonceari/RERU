import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionDropdownTableComponent } from './position-dropdown-table.component';

describe('PositionDropdownTableComponent', () => {
  let component: PositionDropdownTableComponent;
  let fixture: ComponentFixture<PositionDropdownTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionDropdownTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionDropdownTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
