import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversalDeleteModalComponent } from './universal-delete-modal.component';

describe('UniversalDeleteModalComponent', () => {
  let component: UniversalDeleteModalComponent;
  let fixture: ComponentFixture<UniversalDeleteModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversalDeleteModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversalDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
