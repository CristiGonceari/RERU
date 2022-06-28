import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEvaluationListComponent } from './add-evaluation-list.component';

describe('AddEvaluationListComponent', () => {
  let component: AddEvaluationListComponent;
  let fixture: ComponentFixture<AddEvaluationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEvaluationListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEvaluationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
