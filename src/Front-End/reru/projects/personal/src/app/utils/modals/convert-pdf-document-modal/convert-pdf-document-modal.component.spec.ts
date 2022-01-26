import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConvertPdfDocumentModalComponent } from './convert-pdf-document-modal.component';

describe('ConvertPdfDocumentModalComponent', () => {
  let component: ConvertPdfDocumentModalComponent;
  let fixture: ComponentFixture<ConvertPdfDocumentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConvertPdfDocumentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConvertPdfDocumentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
