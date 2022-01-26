import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteNomenclatureModalComponent } from './delete-nomenclature-modal.component';

describe('DeleteNomenclatureModalComponent', () => {
  let component: DeleteNomenclatureModalComponent;
  let fixture: ComponentFixture<DeleteNomenclatureModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteNomenclatureModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteNomenclatureModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
