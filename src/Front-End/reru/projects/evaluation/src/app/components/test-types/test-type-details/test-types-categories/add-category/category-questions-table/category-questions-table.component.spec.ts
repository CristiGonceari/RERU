import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryQuestionsTableComponent } from './category-questions-table.component';

describe('CategoryQuestionsTableComponent', () => {
  let component: CategoryQuestionsTableComponent;
  let fixture: ComponentFixture<CategoryQuestionsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryQuestionsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryQuestionsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
