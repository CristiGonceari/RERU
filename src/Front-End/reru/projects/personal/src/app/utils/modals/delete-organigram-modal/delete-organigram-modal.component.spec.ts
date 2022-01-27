import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteOrganigramModalComponent } from './delete-organigram-modal.component';

describe('DeleteOrganigramModalComponent', () => {
  let component: DeleteOrganigramModalComponent;
  let fixture: ComponentFixture<DeleteOrganigramModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteOrganigramModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteOrganigramModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
