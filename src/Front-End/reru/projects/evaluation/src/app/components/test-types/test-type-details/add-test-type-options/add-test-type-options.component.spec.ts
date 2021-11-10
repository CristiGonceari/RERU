import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestTypeOptionsComponent } from './add-test-type-options.component';

describe('AddTestTypeOptionsComponent', () => {
  let component: AddTestTypeOptionsComponent;
  let fixture: ComponentFixture<AddTestTypeOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestTypeOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestTypeOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
