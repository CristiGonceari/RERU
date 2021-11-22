import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformingPollComponent } from './performing-poll.component';

describe('PerformingPollComponent', () => {
  let component: PerformingPollComponent;
  let fixture: ComponentFixture<PerformingPollComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PerformingPollComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PerformingPollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
