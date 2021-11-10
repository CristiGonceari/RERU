import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestTypeRulesComponent } from './add-test-type-rules.component';

describe('AddTestTypeRulesComponent', () => {
  let component: AddTestTypeRulesComponent;
  let fixture: ComponentFixture<AddTestTypeRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestTypeRulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestTypeRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
