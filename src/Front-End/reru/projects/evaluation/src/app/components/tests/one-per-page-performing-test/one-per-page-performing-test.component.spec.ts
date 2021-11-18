import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnePerPagePerformingTestComponent } from './one-per-page-performing-test.component';

describe('OnePerPagePerformingTestComponent', () => {
  let component: OnePerPagePerformingTestComponent;
  let fixture: ComponentFixture<OnePerPagePerformingTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OnePerPagePerformingTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OnePerPagePerformingTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
