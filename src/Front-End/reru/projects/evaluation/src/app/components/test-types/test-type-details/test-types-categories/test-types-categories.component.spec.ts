import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypesCategoriesComponent } from './test-types-categories.component';

describe('TestTypesCategoriesComponent', () => {
  let component: TestTypesCategoriesComponent;
  let fixture: ComponentFixture<TestTypesCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypesCategoriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypesCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
