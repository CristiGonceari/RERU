import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportRoleModalComponent } from './import-role-modal.component';

describe('ImportRoleModalComponent', () => {
  let component: ImportRoleModalComponent;
  let fixture: ComponentFixture<ImportRoleModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportRoleModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportRoleModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
