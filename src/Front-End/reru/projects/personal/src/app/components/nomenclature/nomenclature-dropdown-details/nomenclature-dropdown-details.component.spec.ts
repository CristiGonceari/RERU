import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureDropdownDetailsComponent } from './nomenclature-dropdown-details.component';

describe('NomenclatureDropdownDetailsComponent', () => {
  let component: NomenclatureDropdownDetailsComponent;
  let fixture: ComponentFixture<NomenclatureDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
