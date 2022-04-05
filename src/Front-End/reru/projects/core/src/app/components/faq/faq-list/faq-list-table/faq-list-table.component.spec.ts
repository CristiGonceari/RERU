import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FaqListTableComponent } from './faq-list-table.component';

describe('FaqListTableComponent', () => {
  let component: FaqListTableComponent;
  let fixture: ComponentFixture<FaqListTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FaqListTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FaqListTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
