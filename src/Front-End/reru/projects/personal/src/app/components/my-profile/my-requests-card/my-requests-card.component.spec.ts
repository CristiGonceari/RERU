import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRequestsCardComponent } from './my-requests-card.component';

describe('MyRequestsCardComponent', () => {
  let component: MyRequestsCardComponent;
  let fixture: ComponentFixture<MyRequestsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyRequestsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyRequestsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
