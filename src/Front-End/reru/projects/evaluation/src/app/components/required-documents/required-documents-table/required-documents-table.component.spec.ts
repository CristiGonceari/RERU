import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequiredDocumentsTableComponent } from './required-documents-table.component';

describe('RequiredDocumentsTableComponent', () => {
  let component: RequiredDocumentsTableComponent;
  let fixture: ComponentFixture<RequiredDocumentsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequiredDocumentsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequiredDocumentsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
