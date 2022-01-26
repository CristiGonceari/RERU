import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRequestByHrModalComponent } from './add-request-by-hr-modal.component';

describe('AddRequestByHrModalComponent', () => {
  let component: AddRequestByHrModalComponent;
  let fixture: ComponentFixture<AddRequestByHrModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRequestByHrModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRequestByHrModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
