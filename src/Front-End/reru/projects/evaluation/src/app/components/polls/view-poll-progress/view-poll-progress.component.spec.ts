import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPollProgressComponent } from './view-poll-progress.component';

describe('ViewPollProgressComponent', () => {
  let component: ViewPollProgressComponent;
  let fixture: ComponentFixture<ViewPollProgressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPollProgressComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPollProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
