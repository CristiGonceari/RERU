import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FaqNameComponent } from './faq-name.component';

describe('FaqNameComponent', () => {
  let component: FaqNameComponent;
  let fixture: ComponentFixture<FaqNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FaqNameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FaqNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
