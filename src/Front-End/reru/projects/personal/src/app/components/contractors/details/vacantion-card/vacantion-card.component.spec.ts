import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacantionCardComponent } from './vacantion-card.component';

describe('VacantionCardComponent', () => {
  let component: VacantionCardComponent;
  let fixture: ComponentFixture<VacantionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacantionCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacantionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
