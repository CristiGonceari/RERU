import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByQuestionTypeComponent } from './search-by-question-type.component';

describe('SearchByQuestionTypeComponent', () => {
  let component: SearchByQuestionTypeComponent;
  let fixture: ComponentFixture<SearchByQuestionTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByQuestionTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByQuestionTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
