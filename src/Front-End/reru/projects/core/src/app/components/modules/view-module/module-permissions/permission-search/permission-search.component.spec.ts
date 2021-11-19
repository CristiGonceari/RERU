import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PermissionSearchComponent } from './permission-search.component';

describe('PermissionSearchComponent', () => {
  let component: PermissionSearchComponent;
  let fixture: ComponentFixture<PermissionSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PermissionSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PermissionSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
