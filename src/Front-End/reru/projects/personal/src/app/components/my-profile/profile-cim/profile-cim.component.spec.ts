import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCimComponent } from './profile-cim.component';

describe('ProfileCimComponent', () => {
  let component: ProfileCimComponent;
  let fixture: ComponentFixture<ProfileCimComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileCimComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileCimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
