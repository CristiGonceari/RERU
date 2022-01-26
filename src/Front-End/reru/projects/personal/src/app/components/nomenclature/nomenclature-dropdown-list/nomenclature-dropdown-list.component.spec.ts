import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NomenclatureDropdownListComponent } from './nomenclature-dropdown-list.component';

describe('NomenclatureDropdownListComponent', () => {
  let component: NomenclatureDropdownListComponent;
  let fixture: ComponentFixture<NomenclatureDropdownListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NomenclatureDropdownListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NomenclatureDropdownListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
