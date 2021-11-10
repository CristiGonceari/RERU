import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypesOverviewComponent } from './test-types-overview.component';

describe('TestTypesOverviewComponent', () => {
  let component: TestTypesOverviewComponent;
  let fixture: ComponentFixture<TestTypesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
