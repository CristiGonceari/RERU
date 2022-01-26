import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureRecordComponent } from './nomenclature-record.component';

describe('NomenclatureRecordComponent', () => {
  let component: NomenclatureRecordComponent;
  let fixture: ComponentFixture<NomenclatureRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
