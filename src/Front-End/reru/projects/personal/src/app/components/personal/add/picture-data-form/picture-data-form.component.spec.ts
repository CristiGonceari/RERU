import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PictureDataFormComponent } from './picture-data-form.component';

describe('PictureDataFormComponent', () => {
  let component: PictureDataFormComponent;
  let fixture: ComponentFixture<PictureDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PictureDataFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PictureDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
