import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacationTableComponent } from './vacation-table.component';

describe('VacationTableComponent', () => {
  let component: VacationTableComponent;
  let fixture: ComponentFixture<VacationTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacationTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacationTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
