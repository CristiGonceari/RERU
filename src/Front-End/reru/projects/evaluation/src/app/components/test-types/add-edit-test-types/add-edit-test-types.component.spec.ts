import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditTestTypesComponent } from './add-edit-test-types.component';

describe('AddEditTestTypesComponent', () => {
  let component: AddEditTestTypesComponent;
  let fixture: ComponentFixture<AddEditTestTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditTestTypesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditTestTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
