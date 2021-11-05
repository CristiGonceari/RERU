import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditQustionComponent } from './add-edit-qustion.component';

describe('AddEditQustionComponent', () => {
  let component: AddEditQustionComponent;
  let fixture: ComponentFixture<AddEditQustionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditQustionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditQustionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
