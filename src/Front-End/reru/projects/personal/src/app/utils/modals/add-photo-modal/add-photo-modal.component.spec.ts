import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPhotoModalComponent } from './add-photo-modal.component';

describe('AddPhotoModalComponent', () => {
  let component: AddPhotoModalComponent;
  let fixture: ComponentFixture<AddPhotoModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPhotoModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPhotoModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
