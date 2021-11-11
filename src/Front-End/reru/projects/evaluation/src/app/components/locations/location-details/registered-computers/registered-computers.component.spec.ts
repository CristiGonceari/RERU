import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisteredComputersComponent } from './registered-computers.component';

describe('RegisteredComputersComponent', () => {
  let component: RegisteredComputersComponent;
  let fixture: ComponentFixture<RegisteredComputersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisteredComputersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisteredComputersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
