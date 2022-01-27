import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PontajDataTableComponent } from './pontaj-data-table.component';

describe('PontajDataTableComponent', () => {
  let component: PontajDataTableComponent;
  let fixture: ComponentFixture<PontajDataTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PontajDataTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PontajDataTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
