import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignFileModalComponent } from './sign-file-modal.component';

describe('SignFileModalComponent', () => {
  let component: SignFileModalComponent;
  let fixture: ComponentFixture<SignFileModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SignFileModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SignFileModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
