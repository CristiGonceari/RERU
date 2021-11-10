import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypeNameComponent } from './test-type-name.component';

describe('TestTypeNameComponent', () => {
  let component: TestTypeNameComponent;
  let fixture: ComponentFixture<TestTypeNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTypeNameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTypeNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
