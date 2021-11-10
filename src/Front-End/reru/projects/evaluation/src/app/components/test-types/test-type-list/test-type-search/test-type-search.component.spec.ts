import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypeSearchComponent } from './test-type-search.component';

describe('TestTypeSearchComponent', () => {
  let component: TestTypeSearchComponent;
  let fixture: ComponentFixture<TestTypeSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypeSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypeSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
