import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewContactDropdownComponent } from './view-contact-dropdown.component';

describe('ViewContactDropdownComponent', () => {
  let component: ViewContactDropdownComponent;
  let fixture: ComponentFixture<ViewContactDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewContactDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewContactDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
