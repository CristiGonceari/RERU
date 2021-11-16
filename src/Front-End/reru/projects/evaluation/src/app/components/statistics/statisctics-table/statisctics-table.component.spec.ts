import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatiscticsTableComponent } from './statisctics-table.component';

describe('StatiscticsTableComponent', () => {
  let component: StatiscticsTableComponent;
  let fixture: ComponentFixture<StatiscticsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatiscticsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatiscticsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
