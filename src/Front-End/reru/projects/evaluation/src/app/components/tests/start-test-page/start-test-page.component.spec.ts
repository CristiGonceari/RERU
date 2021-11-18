import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartTestPageComponent } from './start-test-page.component';

describe('StartTestPageComponent', () => {
  let component: StartTestPageComponent;
  let fixture: ComponentFixture<StartTestPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartTestPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartTestPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
