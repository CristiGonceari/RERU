import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudiesDataFormComponent } from './studies-data-form.component';

describe('StudiesDataFormComponent', () => {
  let component: StudiesDataFormComponent;
  let fixture: ComponentFixture<StudiesDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudiesDataFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudiesDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
