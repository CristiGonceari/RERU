import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileRanksCardComponent } from './profile-ranks-card.component';

describe('ProfileRanksCardComponent', () => {
  let component: ProfileRanksCardComponent;
  let fixture: ComponentFixture<ProfileRanksCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileRanksCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileRanksCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
