import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractorsTableComponent } from './contractors-table.component';

describe('ContractorsTableComponent', () => {
  let component: ContractorsTableComponent;
  let fixture: ComponentFixture<ContractorsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContractorsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractorsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
