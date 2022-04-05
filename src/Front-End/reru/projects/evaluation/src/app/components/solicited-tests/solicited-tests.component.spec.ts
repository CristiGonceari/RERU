import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitedTestsComponent } from './solicited-tests.component';

describe('SolicitedTestsComponent', () => {
  let component: SolicitedTestsComponent;
  let fixture: ComponentFixture<SolicitedTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitedTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitedTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
