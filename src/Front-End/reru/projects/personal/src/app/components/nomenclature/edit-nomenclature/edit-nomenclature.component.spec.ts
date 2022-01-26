import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditNomenclatureComponent } from './edit-nomenclature.component';

describe('EditComponent', () => {
  let component: EditNomenclatureComponent;
  let fixture: ComponentFixture<EditNomenclatureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditNomenclatureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditNomenclatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
