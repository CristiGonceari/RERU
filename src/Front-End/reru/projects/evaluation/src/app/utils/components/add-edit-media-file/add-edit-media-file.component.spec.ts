import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditMediaFileComponent } from './add-edit-media-file.component';

describe('AddEditMediaFileComponent', () => {
  let component: AddEditMediaFileComponent;
  let fixture: ComponentFixture<AddEditMediaFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditMediaFileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditMediaFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
