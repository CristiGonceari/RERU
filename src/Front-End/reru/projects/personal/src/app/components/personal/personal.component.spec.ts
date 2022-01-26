import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalComponent } from './personal.component';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { HttpLoaderFactory, ManualLoaderFactory } from '../../utils/services/i18n.service';
import { HttpClient } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../../../dashboard/src/app/utils/services/i18n.service';
import { LocalizeRouterService, LocalizeRouterModule, LocalizeParser, LocalizeRouterSettings, CacheMechanism } from '@gilsdav/ngx-translate-router';
import { UtilsModule } from '../../utils/utils.module';
import { Location } from '@angular/common';

describe('PersonalComponent', () => {
  let component: PersonalComponent;
  let fixture: ComponentFixture<PersonalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient],
          },
        }),
        LocalizeRouterModule.forRoot([], {
          parser: {
            provide: LocalizeParser,
            useFactory: ManualLoaderFactory,
            deps: [TranslateService, Location, LocalizeRouterSettings],
          },
          cacheMechanism: CacheMechanism.Cookie,
          cookieFormat: '{{value}};{{expires:20}};path=/',
          alwaysSetPrefix: false,
        }),
        NgbModule,
        UtilsModule
      ],
      declarations: [PersonalComponent],
      providers: [I18nService, Location]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
