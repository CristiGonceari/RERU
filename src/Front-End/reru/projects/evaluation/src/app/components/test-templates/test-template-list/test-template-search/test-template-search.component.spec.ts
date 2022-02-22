import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplateSearchComponent } from './test-template-search.component';

describe('TestTemplateSearchComponent', () => {
  let component: TestTemplateSearchComponent;
  let fixture: ComponentFixture<TestTemplateSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplateSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplateSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
