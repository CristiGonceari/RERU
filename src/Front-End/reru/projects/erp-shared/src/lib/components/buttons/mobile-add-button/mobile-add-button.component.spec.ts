import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileAddButtonComponent } from './mobile-add-button.component';

describe('MobileAddButtonComponent', () => {
  let component: MobileAddButtonComponent;
  let fixture: ComponentFixture<MobileAddButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MobileAddButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileAddButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
