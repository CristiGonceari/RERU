import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestHistoryModalComponent } from './add-test-history-modal.component';

describe('AddTestHistoryModalComponent', () => {
  let component: AddTestHistoryModalComponent;
  let fixture: ComponentFixture<AddTestHistoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestHistoryModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
