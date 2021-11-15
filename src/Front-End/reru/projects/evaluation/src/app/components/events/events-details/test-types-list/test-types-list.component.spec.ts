import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypesListComponent } from './test-types-list.component';

describe('TestTypesListComponent', () => {
  let component: TestTypesListComponent;
  let fixture: ComponentFixture<TestTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
