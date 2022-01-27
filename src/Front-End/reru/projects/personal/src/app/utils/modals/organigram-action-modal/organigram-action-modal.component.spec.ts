import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganigramActionModalComponent } from './organigram-action-modal.component';

describe('OrganigramActionModalComponent', () => {
  let component: OrganigramActionModalComponent;
  let fixture: ComponentFixture<OrganigramActionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganigramActionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganigramActionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
