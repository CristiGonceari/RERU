import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPositionDiagramComponent } from './user-position-diagram.component';

describe('UserPositionDiagramComponent', () => {
  let component: UserPositionDiagramComponent;
  let fixture: ComponentFixture<UserPositionDiagramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPositionDiagramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPositionDiagramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
