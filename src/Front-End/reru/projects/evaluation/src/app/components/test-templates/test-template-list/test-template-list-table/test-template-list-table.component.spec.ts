import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplateListTableComponent } from './test-template-list-table.component';

describe('TestTemplateListTableComponent', () => {
  let component: TestTemplateListTableComponent;
  let fixture: ComponentFixture<TestTemplateListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplateListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplateListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
