import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestsByEventComponent } from './tests-by-event.component';

describe('TestsByEventComponent', () => {
  let component: TestsByEventComponent;
  let fixture: ComponentFixture<TestsByEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestsByEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestsByEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
