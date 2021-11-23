import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveModuleAccessComponent } from './remove-module-access.component';

describe('RemoveModuleAccessComponent', () => {
  let component: RemoveModuleAccessComponent;
  let fixture: ComponentFixture<RemoveModuleAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveModuleAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveModuleAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
