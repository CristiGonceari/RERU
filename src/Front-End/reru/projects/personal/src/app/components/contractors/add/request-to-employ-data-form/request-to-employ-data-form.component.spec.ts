import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestToEmployDataFormComponent } from './request-to-employ-data-form.component';

describe('RequestToEmployDataFormComponent', () => {
  let component: RequestToEmployDataFormComponent;
  let fixture: ComponentFixture<RequestToEmployDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestToEmployDataFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestToEmployDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
