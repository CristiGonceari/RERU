import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureStatusLabelComponent } from './nomenclature-status-label.component';

describe('NomenclatureStatusLabelComponent', () => {
  let component: NomenclatureStatusLabelComponent;
  let fixture: ComponentFixture<NomenclatureStatusLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureStatusLabelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureStatusLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
