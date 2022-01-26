import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileRanksComponent } from './profile-ranks.component';

describe('ProfileRanksComponent', () => {
  let component: ProfileRanksComponent;
  let fixture: ComponentFixture<ProfileRanksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileRanksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileRanksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
