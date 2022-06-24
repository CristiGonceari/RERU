import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequiredDocumentsListComponent } from './required-documents-list.component';

describe('RequiredDocumentsListComponent', () => {
  let component: RequiredDocumentsListComponent;
  let fixture: ComponentFixture<RequiredDocumentsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequiredDocumentsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequiredDocumentsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
