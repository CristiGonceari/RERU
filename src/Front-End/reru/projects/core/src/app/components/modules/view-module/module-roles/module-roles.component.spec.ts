import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleRolesComponent } from './module-roles.component';

describe('ModuleRolesComponent', () => {
  let component: ModuleRolesComponent;
  let fixture: ComponentFixture<ModuleRolesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuleRolesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
