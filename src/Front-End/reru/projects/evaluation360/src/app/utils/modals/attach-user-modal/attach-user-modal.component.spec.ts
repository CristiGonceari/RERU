import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachUserModalComponent } from './attach-user-modal.component';

describe('AttachUserModalComponent', () => {
  let component: AttachUserModalComponent;
  let fixture: ComponentFixture<AttachUserModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttachUserModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachUserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
