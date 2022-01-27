import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulletinAddressModalComponent } from './bulletin-address-modal.component';

describe('BulletinAddressModalComponent', () => {
  let component: BulletinAddressModalComponent;
  let fixture: ComponentFixture<BulletinAddressModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulletinAddressModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulletinAddressModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
