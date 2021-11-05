import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionListTableComponent } from './question-list-table.component';

describe('QuestionListTableComponent', () => {
  let component: QuestionListTableComponent;
  let fixture: ComponentFixture<QuestionListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
