import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNomenclatureComponent } from './add-nomenclature.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpLoaderFactory } from '../../../utils/services/i18n.service';
import { HttpClient } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../../utils/utils.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SimpleNotificationsModule } from 'angular2-notifications';

describe('AddComponent', () => {
  let component: AddNomenclatureComponent;
  let fixture: ComponentFixture<AddNomenclatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        SimpleNotificationsModule.forRoot(),
        TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient],
          },
        }),
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        UtilsModule
      ],
      declarations: [AddNomenclatureComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNomenclatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
