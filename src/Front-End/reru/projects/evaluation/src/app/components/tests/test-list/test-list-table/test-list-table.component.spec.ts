import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestListTableComponent } from './test-list-table.component';

describe('TestListTableComponent', () => {
  let component: TestListTableComponent;
  let fixture: ComponentFixture<TestListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
