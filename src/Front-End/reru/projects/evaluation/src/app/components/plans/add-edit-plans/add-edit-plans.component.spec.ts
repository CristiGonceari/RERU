import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditPlansComponent } from './add-edit-plans.component';

describe('AddEditPlansComponent', () => {
  let component: AddEditPlansComponent;
  let fixture: ComponentFixture<AddEditPlansComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditPlansComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditPlansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
