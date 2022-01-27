import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteColumnModalComponent } from './delete-column-modal.component';

describe('DeleteColumnModalComponent', () => {
  let component: DeleteColumnModalComponent;
  let fixture: ComponentFixture<DeleteColumnModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteColumnModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteColumnModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
