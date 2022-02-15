import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByQuestionStatusComponent } from './search-by-question-status.component';

describe('SearchByQuestionStatusComponent', () => {
  let component: SearchByQuestionStatusComponent;
  let fixture: ComponentFixture<SearchByQuestionStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByQuestionStatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByQuestionStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
