import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFamilyModalComponent } from './edit-family-modal.component';

describe('EditFamilyModalComponent', () => {
  let component: EditFamilyModalComponent;
  let fixture: ComponentFixture<EditFamilyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditFamilyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditFamilyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
