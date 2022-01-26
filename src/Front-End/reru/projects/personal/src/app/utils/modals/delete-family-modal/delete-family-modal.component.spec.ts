import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteFamilyModalComponent } from './delete-family-modal.component';

describe('DeleteFamilyModalComponent', () => {
  let component: DeleteFamilyModalComponent;
  let fixture: ComponentFixture<DeleteFamilyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteFamilyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteFamilyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
