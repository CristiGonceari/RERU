import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteOptionComponent } from './delete-option.component';

describe('DeleteOptionComponent', () => {
  let component: DeleteOptionComponent;
  let fixture: ComponentFixture<DeleteOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
