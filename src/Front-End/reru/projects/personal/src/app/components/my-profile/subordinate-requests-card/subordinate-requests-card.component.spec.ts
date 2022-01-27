import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubordinateRequestsCardComponent } from './subordinate-requests-card.component';

describe('SubordinateRequestsCardComponent', () => {
  let component: SubordinateRequestsCardComponent;
  let fixture: ComponentFixture<SubordinateRequestsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubordinateRequestsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubordinateRequestsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
