import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryListTableComponent } from './category-list-table.component';

describe('CategoryListTableComponent', () => {
  let component: CategoryListTableComponent;
  let fixture: ComponentFixture<CategoryListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
