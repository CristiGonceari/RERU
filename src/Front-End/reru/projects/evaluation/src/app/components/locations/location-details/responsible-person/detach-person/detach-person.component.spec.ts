import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetachPersonComponent } from './detach-person.component';

describe('DetachPersonComponent', () => {
  let component: DetachPersonComponent;
  let fixture: ComponentFixture<DetachPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetachPersonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetachPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
