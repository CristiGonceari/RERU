import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutobiographyComponent } from './autobiography.component';

describe('AutobiographyComponent', () => {
  let component: AutobiographyComponent;
  let fixture: ComponentFixture<AutobiographyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutobiographyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AutobiographyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
