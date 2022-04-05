import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchInfoVideoModalComponent } from './watch-info-video-modal.component';

describe('WatchInfoVideoModalComponent', () => {
  let component: WatchInfoVideoModalComponent;
  let fixture: ComponentFixture<WatchInfoVideoModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WatchInfoVideoModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WatchInfoVideoModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
