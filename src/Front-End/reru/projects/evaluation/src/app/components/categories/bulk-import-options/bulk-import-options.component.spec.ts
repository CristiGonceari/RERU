import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkImportOptionsComponent } from './bulk-import-options.component';

describe('BulkImportOptionsComponent', () => {
  let component: BulkImportOptionsComponent;
  let fixture: ComponentFixture<BulkImportOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulkImportOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkImportOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
