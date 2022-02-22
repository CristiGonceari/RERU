import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplateOverviewComponent } from './test-template-overview.component';

describe('TestTemplateOverviewComponent', () => {
  let component: TestTemplateOverviewComponent;
  let fixture: ComponentFixture<TestTemplateOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplateOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplateOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
