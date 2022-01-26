import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRankModalComponent } from './edit-rank-modal.component';

describe('EditRankModalComponent', () => {
  let component: EditRankModalComponent;
  let fixture: ComponentFixture<EditRankModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditRankModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditRankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
