import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditRequiredDocumentComponent } from './add-edit-required-document.component';

describe('AddEditRequiredDocumentComponent', () => {
  let component: AddEditRequiredDocumentComponent;
  let fixture: ComponentFixture<AddEditRequiredDocumentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditRequiredDocumentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditRequiredDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
