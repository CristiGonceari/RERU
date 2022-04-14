import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentTemplatesTableComponent } from './document-templates-table.component';

describe('DocumentTemplatesTableComponent', () => {
  let component: DocumentTemplatesTableComponent;
  let fixture: ComponentFixture<DocumentTemplatesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentTemplatesTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentTemplatesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
