import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNomenclatureHeaderModalComponent } from './add-nomenclature-header-modal.component';

describe('AddNomenclatureHeaderModalComponent', () => {
  let component: AddNomenclatureHeaderModalComponent;
  let fixture: ComponentFixture<AddNomenclatureHeaderModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNomenclatureHeaderModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNomenclatureHeaderModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
