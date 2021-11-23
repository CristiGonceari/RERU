import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterUserStateComponent } from './filter-user-state.component';

describe('FilterUserStateComponent', () => {
  let component: FilterUserStateComponent;
  let fixture: ComponentFixture<FilterUserStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterUserStateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterUserStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
