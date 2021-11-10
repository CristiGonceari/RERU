import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResponsablePersonsTableComponent } from './responsable-persons-table.component';

describe('ResponsablePersonsTableComponent', () => {
  let component: ResponsablePersonsTableComponent;
  let fixture: ComponentFixture<ResponsablePersonsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResponsablePersonsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResponsablePersonsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
