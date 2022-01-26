import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DismissalRequestComponent } from './dismissal-request.component';

describe('DismissalRequestComponent', () => {
  let component: DismissalRequestComponent;
  let fixture: ComponentFixture<DismissalRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DismissalRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DismissalRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
