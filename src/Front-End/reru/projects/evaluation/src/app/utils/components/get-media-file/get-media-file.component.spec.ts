import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetMediaFileComponent } from './get-media-file.component';

describe('GetMediaFileComponent', () => {
  let component: GetMediaFileComponent;
  let fixture: ComponentFixture<GetMediaFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetMediaFileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GetMediaFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
