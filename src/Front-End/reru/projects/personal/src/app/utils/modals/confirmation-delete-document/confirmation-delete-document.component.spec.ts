import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationDeleteDocumentComponent } from './confirmation-delete-document.component';

describe('ConfirmationDeleteDocumentComponent', () => {
  let component: ConfirmationDeleteDocumentComponent;
  let fixture: ComponentFixture<ConfirmationDeleteDocumentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmationDeleteDocumentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmationDeleteDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
