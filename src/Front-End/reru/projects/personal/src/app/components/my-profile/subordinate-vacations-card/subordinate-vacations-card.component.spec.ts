import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubordinateVacationsCardComponent } from './subordinate-vacations-card.component';

describe('SubordinateVacationsCardComponent', () => {
  let component: SubordinateVacationsCardComponent;
  let fixture: ComponentFixture<SubordinateVacationsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubordinateVacationsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubordinateVacationsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
