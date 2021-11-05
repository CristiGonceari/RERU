import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkEditorConfigComponent } from './ck-editor-config.component';

describe('CkEditorConfigComponent', () => {
  let component: CkEditorConfigComponent;
  let fixture: ComponentFixture<CkEditorConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CkEditorConfigComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CkEditorConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
