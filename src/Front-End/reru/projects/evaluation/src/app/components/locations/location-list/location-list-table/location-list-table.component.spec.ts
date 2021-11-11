import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationListTableComponent } from './location-list-table.component';

describe('LocationListTableComponent', () => {
  let component: LocationListTableComponent;
  let fixture: ComponentFixture<LocationListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LocationListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
