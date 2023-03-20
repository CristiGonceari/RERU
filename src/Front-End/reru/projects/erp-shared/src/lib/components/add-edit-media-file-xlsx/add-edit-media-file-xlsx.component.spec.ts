import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddEditMediaFileXlsxComponent } from './add-edit-media-file-xlsx.component';

describe('AddEditMediaFileXlsxComponent', () => {
  let component: AddEditMediaFileXlsxComponent;
  let fixture: ComponentFixture<AddEditMediaFileXlsxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditMediaFileXlsxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditMediaFileXlsxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});