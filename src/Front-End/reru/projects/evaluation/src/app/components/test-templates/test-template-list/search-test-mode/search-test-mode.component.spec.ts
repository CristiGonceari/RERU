import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTestModeComponent } from './search-test-mode.component';

describe('SearchTestModeComponent', () => {
  let component: SearchTestModeComponent;
  let fixture: ComponentFixture<SearchTestModeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchTestModeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchTestModeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
