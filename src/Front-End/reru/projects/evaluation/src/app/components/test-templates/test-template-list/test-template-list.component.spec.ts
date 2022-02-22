import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTemplateListComponent } from './test-template-list.component';

describe('TestTemplateListComponent', () => {
  let component: TestTemplateListComponent;
  let fixture: ComponentFixture<TestTemplateListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTemplateListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTemplateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
