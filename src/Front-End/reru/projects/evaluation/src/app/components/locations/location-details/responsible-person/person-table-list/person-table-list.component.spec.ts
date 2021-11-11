import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonTableListComponent } from './person-table-list.component';

describe('PersonTableListComponent', () => {
  let component: PersonTableListComponent;
  let fixture: ComponentFixture<PersonTableListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonTableListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonTableListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
