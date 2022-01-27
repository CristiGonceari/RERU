import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileStudiesComponent } from './profile-studies.component';

describe('ProfileStudiesComponent', () => {
  let component: ProfileStudiesComponent;
  let fixture: ComponentFixture<ProfileStudiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileStudiesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileStudiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
