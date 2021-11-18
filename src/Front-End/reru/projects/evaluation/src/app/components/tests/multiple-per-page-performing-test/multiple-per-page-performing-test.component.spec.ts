import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiplePerPagePerformingTestComponent } from './multiple-per-page-performing-test.component';

describe('MultiplePerPagePerformingTestComponent', () => {
  let component: MultiplePerPagePerformingTestComponent;
  let fixture: ComponentFixture<MultiplePerPagePerformingTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultiplePerPagePerformingTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultiplePerPagePerformingTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
