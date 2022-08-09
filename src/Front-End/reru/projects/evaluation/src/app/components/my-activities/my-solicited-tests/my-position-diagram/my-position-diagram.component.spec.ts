import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyPositionDiagramComponent } from './my-position-diagram.component';

describe('MyPositionDiagramComponent', () => {
  let component: MyPositionDiagramComponent;
  let fixture: ComponentFixture<MyPositionDiagramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyPositionDiagramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyPositionDiagramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
