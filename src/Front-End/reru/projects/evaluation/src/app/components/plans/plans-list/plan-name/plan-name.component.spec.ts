import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanNameComponent } from './plan-name.component';

describe('PlanNameComponent', () => {
  let component: PlanNameComponent;
  let fixture: ComponentFixture<PlanNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanNameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
