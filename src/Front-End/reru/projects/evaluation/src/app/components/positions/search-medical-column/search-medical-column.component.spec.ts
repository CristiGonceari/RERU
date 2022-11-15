import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchMedicalColumnComponent } from './search-medical-column.component';

describe('SearchMedicalColumnComponent', () => {
  let component: SearchMedicalColumnComponent;
  let fixture: ComponentFixture<SearchMedicalColumnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchMedicalColumnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchMedicalColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
