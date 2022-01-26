import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentContentModalComponent } from './department-content-modal.component';

describe('DepartmentContentModalComponent', () => {
  let component: DepartmentContentModalComponent;
  let fixture: ComponentFixture<DepartmentContentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentContentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentContentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
