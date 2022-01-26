import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByContractorsComponent } from './search-by-contractors.component';

describe('SearchByContractorsComponent', () => {
  let component: SearchByContractorsComponent;
  let fixture: ComponentFixture<SearchByContractorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByContractorsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByContractorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
