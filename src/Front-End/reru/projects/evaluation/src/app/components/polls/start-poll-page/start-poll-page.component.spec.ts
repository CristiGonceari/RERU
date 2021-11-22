import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartPollPageComponent } from './start-poll-page.component';

describe('StartPollPageComponent', () => {
  let component: StartPollPageComponent;
  let fixture: ComponentFixture<StartPollPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartPollPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartPollPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
