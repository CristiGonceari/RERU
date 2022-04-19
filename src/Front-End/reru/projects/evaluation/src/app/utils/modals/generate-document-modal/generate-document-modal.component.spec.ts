import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateDocumentModalComponent } from './generate-document-modal.component';

describe('GenerateDocumentModalComponent', () => {
  let component: GenerateDocumentModalComponent;
  let fixture: ComponentFixture<GenerateDocumentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenerateDocumentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateDocumentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
