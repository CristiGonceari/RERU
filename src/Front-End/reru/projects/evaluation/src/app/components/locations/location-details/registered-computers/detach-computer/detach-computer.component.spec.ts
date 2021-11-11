import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetachComputerComponent } from './detach-computer.component';

describe('DetachComputerComponent', () => {
  let component: DetachComputerComponent;
  let fixture: ComponentFixture<DetachComputerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetachComputerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetachComputerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
