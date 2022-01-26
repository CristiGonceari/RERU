import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStydyModalComponent } from './edit-stydy-modal.component';

describe('EditStydyModalComponent', () => {
  let component: EditStydyModalComponent;
  let fixture: ComponentFixture<EditStydyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditStydyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStydyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
