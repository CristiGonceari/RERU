import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryQuestionsComponent } from './category-questions.component';

describe('CategoryQuestionsComponent', () => {
  let component: CategoryQuestionsComponent;
  let fixture: ComponentFixture<CategoryQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
