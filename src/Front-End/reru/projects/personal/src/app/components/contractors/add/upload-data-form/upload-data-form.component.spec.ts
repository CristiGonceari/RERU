import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadDataFormComponent } from './upload-data-form.component';

describe('UploadDataFormComponent', () => {
  let component: UploadDataFormComponent;
  let fixture: ComponentFixture<UploadDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadDataFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
