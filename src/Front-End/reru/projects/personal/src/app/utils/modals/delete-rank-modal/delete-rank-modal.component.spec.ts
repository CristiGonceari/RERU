import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRankModalComponent } from './delete-rank-modal.component';

describe('DeleteRankModalComponent', () => {
  let component: DeleteRankModalComponent;
  let fixture: ComponentFixture<DeleteRankModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteRankModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteRankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
