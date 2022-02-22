import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplatesRulesComponent } from './test-template-rules.component';

describe('TestTemplatesRulesComponent', () => {
  let component: TestTemplatesRulesComponent;
  let fixture: ComponentFixture<TestTemplatesRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplatesRulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplatesRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
