import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewJsonModalComponent } from './view-json-modal.component';

describe('ViewJsonModalComponent', () => {
  let component: ViewJsonModalComponent;
  let fixture: ComponentFixture<ViewJsonModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewJsonModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewJsonModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
