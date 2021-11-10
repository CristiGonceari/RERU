import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachPersonsComponent } from './attach-persons.component';

describe('AttachPersonsComponent', () => {
  let component: AttachPersonsComponent;
  let fixture: ComponentFixture<AttachPersonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttachPersonsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachPersonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
