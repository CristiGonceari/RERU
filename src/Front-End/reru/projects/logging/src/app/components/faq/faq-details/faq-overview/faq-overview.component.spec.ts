import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FaqOverviewComponent } from './faq-overview.component';

describe('FaqOverviewComponent', () => {
  let component: FaqOverviewComponent;
  let fixture: ComponentFixture<FaqOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FaqOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FaqOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
