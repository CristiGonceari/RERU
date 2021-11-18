import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachPersonComponent } from './attach-person.component';

describe('AttachPersonComponent', () => {
  let component: AttachPersonComponent;
  let fixture: ComponentFixture<AttachPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttachPersonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
