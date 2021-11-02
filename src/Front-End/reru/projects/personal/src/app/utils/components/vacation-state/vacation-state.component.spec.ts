import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacationStateComponent } from './vacation-state.component';

describe('VacationStateComponent', () => {
  let component: VacationStateComponent;
  let fixture: ComponentFixture<VacationStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacationStateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacationStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
