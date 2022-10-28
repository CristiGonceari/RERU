import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoToTestModalComponent } from './go-to-test-modal.component';

describe('GoToTestModalComponent', () => {
  let component: GoToTestModalComponent;
  let fixture: ComponentFixture<GoToTestModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GoToTestModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GoToTestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
