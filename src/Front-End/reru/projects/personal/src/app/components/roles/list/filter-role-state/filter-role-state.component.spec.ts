import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterRoleStateComponent } from './filter-role-state.component';

describe('FilterRoleStateComponent', () => {
  let component: FilterRoleStateComponent;
  let fixture: ComponentFixture<FilterRoleStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterRoleStateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterRoleStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
