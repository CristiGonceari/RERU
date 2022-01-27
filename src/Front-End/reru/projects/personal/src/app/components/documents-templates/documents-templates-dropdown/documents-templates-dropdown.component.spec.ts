import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsTemplatesDropdownComponent } from './documents-templates-dropdown.component';

describe('DocumentsTemplatesDropdownComponent', () => {
  let component: DocumentsTemplatesDropdownComponent;
  let fixture: ComponentFixture<DocumentsTemplatesDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentsTemplatesDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentsTemplatesDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
