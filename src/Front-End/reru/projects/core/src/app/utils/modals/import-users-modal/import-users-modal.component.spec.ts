import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportUsersModalComponent } from './import-users-modal.component';

describe('ImportUsersModalComponent', () => {
  let component: ImportUsersModalComponent;
  let fixture: ComponentFixture<ImportUsersModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportUsersModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportUsersModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
