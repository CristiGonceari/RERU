import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryObligationComponent } from './military-obligation.component';

describe('MilitaryObligationComponent', () => {
  let component: MilitaryObligationComponent;
  let fixture: ComponentFixture<MilitaryObligationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryObligationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryObligationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
