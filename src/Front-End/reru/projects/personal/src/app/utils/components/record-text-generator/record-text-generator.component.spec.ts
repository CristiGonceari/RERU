import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordTextGeneratorComponent } from './record-text-generator.component';

describe('RecordTextGeneratorComponent', () => {
  let component: RecordTextGeneratorComponent;
  let fixture: ComponentFixture<RecordTextGeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordTextGeneratorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordTextGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
