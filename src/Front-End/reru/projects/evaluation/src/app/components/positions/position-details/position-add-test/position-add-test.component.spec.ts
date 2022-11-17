import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionAddTestComponent } from './position-add-test.component';

describe('PositionAddTestComponent', () => {
  let component: PositionAddTestComponent;
  let fixture: ComponentFixture<PositionAddTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionAddTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionAddTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
