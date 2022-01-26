import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DismissalRequestCardComponent } from './dismissal-request-card.component';

describe('DismissalRequestCardComponent', () => {
  let component: DismissalRequestCardComponent;
  let fixture: ComponentFixture<DismissalRequestCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DismissalRequestCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DismissalRequestCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
