import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchQualifyingTypeComponent } from './search-qualifying-type.component';

describe('SearchQualifyingTypeComponent', () => {
  let component: SearchQualifyingTypeComponent;
  let fixture: ComponentFixture<SearchQualifyingTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchQualifyingTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchQualifyingTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
