import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplatesCategoriesComponent } from './test-templates-categories.component';

describe('TestTemplatesCategoriesComponent', () => {
  let component: TestTemplatesCategoriesComponent;
  let fixture: ComponentFixture<TestTemplatesCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplatesCategoriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplatesCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
