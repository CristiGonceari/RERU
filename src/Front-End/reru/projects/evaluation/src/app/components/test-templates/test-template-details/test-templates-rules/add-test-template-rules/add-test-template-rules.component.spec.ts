import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestTemplateRulesComponent } from './add-test-template-rules.component';

describe('AddTestTemplateRulesComponent', () => {
  let component: AddTestTemplateRulesComponent;
  let fixture: ComponentFixture<AddTestTemplateRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestTemplateRulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestTemplateRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
