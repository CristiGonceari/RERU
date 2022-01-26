import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulletinDataFormComponent } from './bulletin-data-form.component';

describe('BulletinDataFormComponent', () => {
  let component: BulletinDataFormComponent;
  let fixture: ComponentFixture<BulletinDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulletinDataFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulletinDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
