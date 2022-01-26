import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNomenclatureRecordModalComponent } from './add-nomenclature-record-modal.component';

describe('AddNomenclatureRecordModalComponent', () => {
  let component: AddNomenclatureRecordModalComponent;
  let fixture: ComponentFixture<AddNomenclatureRecordModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNomenclatureRecordModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNomenclatureRecordModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
