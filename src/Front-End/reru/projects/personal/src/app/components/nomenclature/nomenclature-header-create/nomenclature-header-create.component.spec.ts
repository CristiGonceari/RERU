import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureHeaderCreateComponent } from './nomenclature-header-create.component';

describe('NomenclatureHeaderCreateComponent', () => {
  let component: NomenclatureHeaderCreateComponent;
  let fixture: ComponentFixture<NomenclatureHeaderCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureHeaderCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureHeaderCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
