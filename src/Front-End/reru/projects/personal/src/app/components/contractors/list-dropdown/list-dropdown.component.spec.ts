import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListDropdownComponent } from './list-dropdown.component';

describe('ListDropdownComponent', () => {
  let component: ListDropdownComponent;
  let fixture: ComponentFixture<ListDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
