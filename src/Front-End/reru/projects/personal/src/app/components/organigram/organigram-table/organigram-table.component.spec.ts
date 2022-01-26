import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganigramTableComponent } from './organigram-table.component';

describe('OrganigramTableComponent', () => {
  let component: OrganigramTableComponent;
  let fixture: ComponentFixture<OrganigramTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganigramTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganigramTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
