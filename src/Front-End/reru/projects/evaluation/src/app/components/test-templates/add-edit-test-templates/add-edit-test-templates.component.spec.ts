import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditTestTemplateComponent } from './add-edit-test-templates.component';

describe('AddEditTestTemplateComponent', () => {
  let component: AddEditTestTemplateComponent;
  let fixture: ComponentFixture<AddEditTestTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditTestTemplateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditTestTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
