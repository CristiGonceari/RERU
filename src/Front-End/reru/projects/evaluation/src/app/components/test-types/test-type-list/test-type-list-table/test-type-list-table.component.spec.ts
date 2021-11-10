import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypeListTableComponent } from './test-type-list-table.component';

describe('TestTypeListTableComponent', () => {
  let component: TestTypeListTableComponent;
  let fixture: ComponentFixture<TestTypeListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypeListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypeListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
