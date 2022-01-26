import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubordinateVacationsComponent } from './subordinate-vacations.component';

describe('SubordinateVacationsComponent', () => {
  let component: SubordinateVacationsComponent;
  let fixture: ComponentFixture<SubordinateVacationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubordinateVacationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubordinateVacationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
