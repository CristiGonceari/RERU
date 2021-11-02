import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastOrganizationRoleLabelComponent } from './last-organization-role-label.component';

describe('LastOrganizationRoleLabelComponent', () => {
  let component: LastOrganizationRoleLabelComponent;
  let fixture: ComponentFixture<LastOrganizationRoleLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastOrganizationRoleLabelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastOrganizationRoleLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
