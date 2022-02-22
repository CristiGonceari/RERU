import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestListComponent } from './add-test-list.component';

describe('AddTestListComponent', () => {
  let component: AddTestListComponent;
  let fixture: ComponentFixture<AddTestListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
