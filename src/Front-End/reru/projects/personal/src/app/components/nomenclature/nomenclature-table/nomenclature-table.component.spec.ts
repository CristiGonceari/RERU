import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureTableComponent } from './nomenclature-table.component';

describe('NomenclatureTableComponent', () => {
  let component: NomenclatureTableComponent;
  let fixture: ComponentFixture<NomenclatureTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
