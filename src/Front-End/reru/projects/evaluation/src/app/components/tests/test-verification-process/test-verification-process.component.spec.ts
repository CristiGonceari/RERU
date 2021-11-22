import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestVerificationProcessComponent } from './test-verification-process.component';

describe('TestVerificationProcessComponent', () => {
  let component: TestVerificationProcessComponent;
  let fixture: ComponentFixture<TestVerificationProcessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestVerificationProcessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestVerificationProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
