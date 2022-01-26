import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacantionsComponent } from './vacantions.component';

describe('VacantionsComponent', () => {
  let component: VacantionsComponent;
  let fixture: ComponentFixture<VacantionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacantionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacantionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
