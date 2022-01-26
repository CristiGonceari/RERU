import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateOrderComponent } from './generate-order.component';

describe('AddPositionComponent', () => {
  let component: GenerateOrderComponent;
  let fixture: ComponentFixture<GenerateOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenerateOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
