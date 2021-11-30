import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryQuestionsOptionsComponent } from './category-questions-options.component';

describe('CategoryQuestionsOptionsComponent', () => {
  let component: CategoryQuestionsOptionsComponent;
  let fixture: ComponentFixture<CategoryQuestionsOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryQuestionsOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryQuestionsOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
