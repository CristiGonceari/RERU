import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceivedTableComponent } from './received-table.component';

describe('ReceivedTableComponent', () => {
  let component: ReceivedTableComponent;
  let fixture: ComponentFixture<ReceivedTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceivedTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceivedTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
