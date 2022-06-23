import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByMandatoryComponent } from './search-by-mandatory.component';

describe('SearchByMandatoryComponent', () => {
  let component: SearchByMandatoryComponent;
  let fixture: ComponentFixture<SearchByMandatoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByMandatoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByMandatoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
