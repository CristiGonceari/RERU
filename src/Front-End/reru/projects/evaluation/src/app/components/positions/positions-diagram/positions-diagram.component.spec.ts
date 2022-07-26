import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionsDiagramComponent } from './positions-diagram.component';

describe('PositionsDiagramComponent', () => {
  let component: PositionsDiagramComponent;
  let fixture: ComponentFixture<PositionsDiagramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionsDiagramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionsDiagramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
