import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditSolicitedTestComponent } from './add-edit-solicited-test.component';

describe('AddSolicitedTestComponent', () => {
  let component: AddEditSolicitedTestComponent;
  let fixture: ComponentFixture<AddEditSolicitedTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditSolicitedTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditSolicitedTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
