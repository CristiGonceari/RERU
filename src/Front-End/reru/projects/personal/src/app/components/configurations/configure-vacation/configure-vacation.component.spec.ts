import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureVacationComponent } from './configure-vacation.component';

describe('ConfigureVacationComponent', () => {
  let component: ConfigureVacationComponent;
  let fixture: ComponentFixture<ConfigureVacationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureVacationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigureVacationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
