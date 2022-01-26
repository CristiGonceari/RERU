import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRankModalComponent } from './add-rank-modal.component';

describe('AddRankModalComponent', () => {
  let component: AddRankModalComponent;
  let fixture: ComponentFixture<AddRankModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRankModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
