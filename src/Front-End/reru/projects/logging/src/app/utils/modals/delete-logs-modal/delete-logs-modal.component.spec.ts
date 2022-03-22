import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteLogsModalComponent } from './delete-logs-modal.component';

describe('DeleteLogsModalComponent', () => {
  let component: DeleteLogsModalComponent;
  let fixture: ComponentFixture<DeleteLogsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteLogsModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteLogsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
