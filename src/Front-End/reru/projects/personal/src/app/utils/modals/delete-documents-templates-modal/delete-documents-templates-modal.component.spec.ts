import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDocumentsTemplatesModalComponent } from './delete-documents-templates-modal.component';

describe('DeleteDocumentsTemplatesModalComponent', () => {
  let component: DeleteDocumentsTemplatesModalComponent;
  let fixture: ComponentFixture<DeleteDocumentsTemplatesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteDocumentsTemplatesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDocumentsTemplatesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
