import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyEvaluatedTestsComponent } from './my-evaluated-tests.component';

describe('MyEvaluatedTestsComponent', () => {
  let component: MyEvaluatedTestsComponent;
  let fixture: ComponentFixture<MyEvaluatedTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyEvaluatedTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyEvaluatedTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
