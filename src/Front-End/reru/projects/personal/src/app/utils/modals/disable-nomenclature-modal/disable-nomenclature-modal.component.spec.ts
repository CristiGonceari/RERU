import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisableNomenclatureModalComponent } from './disable-nomenclature-modal.component';

describe('DisableNomenclatureModalComponent', () => {
  let component: DisableNomenclatureModalComponent;
  let fixture: ComponentFixture<DisableNomenclatureModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisableNomenclatureModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisableNomenclatureModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
