import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsTemplatesTableComponent } from './documents-templates-table.component';

describe('DocumentsTemplatesTableComponent', () => {
  let component: DocumentsTemplatesTableComponent;
  let fixture: ComponentFixture<DocumentsTemplatesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentsTemplatesTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentsTemplatesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
