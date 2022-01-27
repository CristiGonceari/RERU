import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyVacationsComponent } from './my-vacations.component';

describe('MyVacationsComponent', () => {
  let component: MyVacationsComponent;
  let fixture: ComponentFixture<MyVacationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyVacationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyVacationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
