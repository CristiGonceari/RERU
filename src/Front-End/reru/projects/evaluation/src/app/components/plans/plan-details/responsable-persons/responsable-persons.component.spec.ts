import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResponsablePersonsComponent } from './responsable-persons.component';

describe('ResponsablePersonsComponent', () => {
  let component: ResponsablePersonsComponent;
  let fixture: ComponentFixture<ResponsablePersonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResponsablePersonsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResponsablePersonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
