import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePositionsComponent } from './profile-positions.component';

describe('ProfilePositionsComponent', () => {
  let component: ProfilePositionsComponent;
  let fixture: ComponentFixture<ProfilePositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilePositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
