import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplateDetailsComponent } from './test-template-details.component';

describe('TestTemplateDetailsComponent', () => {
  let component: TestTemplateDetailsComponent;
  let fixture: ComponentFixture<TestTemplateDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplateDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplateDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
