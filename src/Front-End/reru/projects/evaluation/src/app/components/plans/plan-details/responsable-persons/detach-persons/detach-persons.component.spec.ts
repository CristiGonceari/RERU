import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetachPersonsComponent } from './detach-persons.component';

describe('DetachPersonsComponent', () => {
  let component: DetachPersonsComponent;
  let fixture: ComponentFixture<DetachPersonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetachPersonsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetachPersonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
