import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubordinateRequestsComponent } from './subordinate-requests.component';

describe('SubordinateRequestsComponent', () => {
  let component: SubordinateRequestsComponent;
  let fixture: ComponentFixture<SubordinateRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubordinateRequestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubordinateRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
