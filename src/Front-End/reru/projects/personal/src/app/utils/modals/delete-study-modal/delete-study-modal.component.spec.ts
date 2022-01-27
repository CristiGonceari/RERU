import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteStudyModalComponent } from './delete-study-modal.component';

describe('DeleteStudyModalComponent', () => {
  let component: DeleteStudyModalComponent;
  let fixture: ComponentFixture<DeleteStudyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteStudyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteStudyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
