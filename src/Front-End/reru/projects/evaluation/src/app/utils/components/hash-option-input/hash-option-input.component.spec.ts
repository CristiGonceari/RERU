import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HashOptionInputComponent } from './hash-option-input.component';

describe('HashOptionInputComponent', () => {
  let component: HashOptionInputComponent;
  let fixture: ComponentFixture<HashOptionInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HashOptionInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HashOptionInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
