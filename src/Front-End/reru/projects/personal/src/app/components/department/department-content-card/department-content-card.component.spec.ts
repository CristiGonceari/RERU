import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentContentCardComponent } from './department-content-card.component';

describe('DepartmentContentCardComponent', () => {
  let component: DepartmentContentCardComponent;
  let fixture: ComponentFixture<DepartmentContentCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentContentCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentContentCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
