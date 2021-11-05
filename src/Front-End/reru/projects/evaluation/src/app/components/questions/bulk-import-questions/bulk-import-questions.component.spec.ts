import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkImportQuestionsComponent } from './bulk-import-questions.component';

describe('BulkImportQuestionsComponent', () => {
  let component: BulkImportQuestionsComponent;
  let fixture: ComponentFixture<BulkImportQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulkImportQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkImportQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
