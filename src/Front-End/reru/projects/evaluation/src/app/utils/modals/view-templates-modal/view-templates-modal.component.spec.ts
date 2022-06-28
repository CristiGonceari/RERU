import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewTemplatesModalComponent } from './view-templates-modal.component';

describe('ViewTemplatesModalComponent', () => {
  let component: ViewTemplatesModalComponent;
  let fixture: ComponentFixture<ViewTemplatesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewTemplatesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewTemplatesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
