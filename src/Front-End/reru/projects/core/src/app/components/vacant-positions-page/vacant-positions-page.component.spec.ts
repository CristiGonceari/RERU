import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacantPositionsPageComponent } from './vacant-positions-page.component';

describe('VacantPositionsPageComponent', () => {
  let component: VacantPositionsPageComponent;
  let fixture: ComponentFixture<VacantPositionsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacantPositionsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacantPositionsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
