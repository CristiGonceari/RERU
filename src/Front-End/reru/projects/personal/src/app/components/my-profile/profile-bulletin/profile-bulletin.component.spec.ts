import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileBulletinComponent } from './profile-bulletin.component';

describe('ProfileBulletinComponent', () => {
  let component: ProfileBulletinComponent;
  let fixture: ComponentFixture<ProfileBulletinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileBulletinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileBulletinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
