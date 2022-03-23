import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MySolicitedTestsComponent } from './my-solicited-tests.component';

describe('MySolicitedTestsComponent', () => {
  let component: MySolicitedTestsComponent;
  let fixture: ComponentFixture<MySolicitedTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MySolicitedTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MySolicitedTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
