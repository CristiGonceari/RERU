import { TestBed } from '@angular/core/testing';

import { I18nService, HttpLoaderFactory, ManualLoaderFactory } from './i18n.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { LocalizeRouterService, LocalizeRouterModule, LocalizeParser, LocalizeRouterSettings, CacheMechanism } from '@gilsdav/ngx-translate-router';
import { Location } from '@angular/common';
import { RouterTestingModule } from '@angular/router/testing';

describe('I18nService', () => {
  let service: I18nService;

  beforeEach(() => {
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
        })
      ],
      providers: [Location]
    });
    service = TestBed.inject(I18nService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
