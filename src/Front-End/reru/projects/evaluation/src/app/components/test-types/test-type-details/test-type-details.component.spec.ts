import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypeDetailsComponent } from './test-type-details.component';

describe('TestTypeDetailsComponent', () => {
  let component: TestTypeDetailsComponent;
  let fixture: ComponentFixture<TestTypeDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypeDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
