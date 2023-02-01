import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Exception404Component } from './404.component';

describe('Exception404Component', () => {
  let component: Exception404Component;
  let fixture: ComponentFixture<Exception404Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [Exception404Component],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Exception404Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
