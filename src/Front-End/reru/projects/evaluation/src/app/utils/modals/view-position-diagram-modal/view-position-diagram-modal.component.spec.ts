import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPositionDiagramModalComponent } from './view-position-diagram-modal.component';

describe('ViewPositionDiagramModalComponent', () => {
  let component: ViewPositionDiagramModalComponent;
  let fixture: ComponentFixture<ViewPositionDiagramModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPositionDiagramModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPositionDiagramModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
