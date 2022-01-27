import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFamilyModalComponent } from './add-family-modal.component';

describe('AddFamilyModalComponent', () => {
  let component: AddFamilyModalComponent;
  let fixture: ComponentFixture<AddFamilyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFamilyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFamilyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
