import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveSolicitedTestComponent } from './approve-solicited-test.component';

describe('ApproveSolicitedTestComponent', () => {
  let component: ApproveSolicitedTestComponent;
  let fixture: ComponentFixture<ApproveSolicitedTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApproveSolicitedTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveSolicitedTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
