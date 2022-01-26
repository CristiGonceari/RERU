import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureRecordCreateComponent } from './nomenclature-record-create.component';

describe('NomenclatureRecordCreateComponent', () => {
  let component: NomenclatureRecordCreateComponent;
  let fixture: ComponentFixture<NomenclatureRecordCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureRecordCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureRecordCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
