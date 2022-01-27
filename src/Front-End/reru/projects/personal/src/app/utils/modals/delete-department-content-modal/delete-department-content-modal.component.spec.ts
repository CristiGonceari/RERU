import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDepartmentContentModalComponent } from './delete-department-content-modal.component';

describe('DeleteDepartmentContentModalComponent', () => {
  let component: DeleteDepartmentContentModalComponent;
  let fixture: ComponentFixture<DeleteDepartmentContentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteDepartmentContentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDepartmentContentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
