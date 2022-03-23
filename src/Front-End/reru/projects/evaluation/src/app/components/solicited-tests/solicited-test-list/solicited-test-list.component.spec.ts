import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitedTestListComponent } from './solicited-test-list.component';

describe('SolicitedTestListComponent', () => {
  let component: SolicitedTestListComponent;
  let fixture: ComponentFixture<SolicitedTestListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitedTestListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitedTestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
