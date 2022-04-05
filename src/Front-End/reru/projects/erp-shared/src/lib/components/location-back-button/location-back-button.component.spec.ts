import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationBackButtonComponent } from './location-back-button.component';

describe('LocationBackButtonComponent', () => {
  let component: LocationBackButtonComponent;
  let fixture: ComponentFixture<LocationBackButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LocationBackButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationBackButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
