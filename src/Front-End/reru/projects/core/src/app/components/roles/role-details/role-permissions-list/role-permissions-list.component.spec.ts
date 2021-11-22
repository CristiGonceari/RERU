import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolePermissionsListComponent } from './role-permissions-list.component';

describe('RolePermissionsListComponent', () => {
  let component: RolePermissionsListComponent;
  let fixture: ComponentFixture<RolePermissionsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RolePermissionsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RolePermissionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
