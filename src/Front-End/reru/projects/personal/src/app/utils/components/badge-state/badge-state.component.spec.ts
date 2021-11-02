import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadgeStateComponent } from './badge-state.component';

describe('BadgeStateComponent', () => {
  let component: BadgeStateComponent;
  let fixture: ComponentFixture<BadgeStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BadgeStateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BadgeStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
