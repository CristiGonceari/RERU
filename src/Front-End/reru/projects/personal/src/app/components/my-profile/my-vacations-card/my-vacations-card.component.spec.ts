import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyVacationsCardComponent } from './my-vacations-card.component';

describe('MyVacationsCardComponent', () => {
  let component: MyVacationsCardComponent;
  let fixture: ComponentFixture<MyVacationsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyVacationsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyVacationsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
