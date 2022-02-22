import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestTemplateOptionsComponent } from './add-test-template-options.component';

describe('AddTestTemplateOptionsComponent', () => {
  let component: AddTestTemplateOptionsComponent;
  let fixture: ComponentFixture<AddTestTemplateOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestTemplateOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestTemplateOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
