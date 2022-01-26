import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsNotFoundComponent } from './documents-not-found.component';

describe('DocumentsNotFoundComponent', () => {
  let component: DocumentsNotFoundComponent;
  let fixture: ComponentFixture<DocumentsNotFoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentsNotFoundComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentsNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
