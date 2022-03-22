import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplatesListComponent } from './test-templates-list.component';

describe('TestTemplatesListComponent', () => {
  let component: TestTemplatesListComponent;
  let fixture: ComponentFixture<TestTemplatesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplatesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplatesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
