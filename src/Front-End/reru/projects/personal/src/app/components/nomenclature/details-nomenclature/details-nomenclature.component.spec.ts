import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsNomenclatureComponent } from './details-nomenclature.component';

describe('DetailsNomenclatureComponent', () => {
  let component: DetailsNomenclatureComponent;
  let fixture: ComponentFixture<DetailsNomenclatureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsNomenclatureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsNomenclatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
