import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyPositionDetailsComponent } from './my-position-details.component';

describe('MyPositionDiagramComponent', () => {
  let component: MyPositionDetailsComponent;
  let fixture: ComponentFixture<MyPositionDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyPositionDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyPositionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
