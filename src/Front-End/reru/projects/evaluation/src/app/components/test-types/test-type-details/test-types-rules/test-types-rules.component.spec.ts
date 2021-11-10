import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypesRulesComponent } from './test-types-rules.component';

describe('TestTypesRulesComponent', () => {
  let component: TestTypesRulesComponent;
  let fixture: ComponentFixture<TestTypesRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypesRulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypesRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
