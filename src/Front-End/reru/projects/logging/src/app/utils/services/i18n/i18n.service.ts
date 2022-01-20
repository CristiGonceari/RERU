import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateService } from '@ngx-translate/core';
import {
  ManualParserLoader,
  LocalizeRouterSettings,
  LocalizeRouterService,
} from '@gilsdav/ngx-translate-router';
import { CookieService } from 'ngx-cookie-service';

import { environment } from '../../../../environments/environment';

import { BehaviorSubject, Observable, forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';

import {
  LOCALIZE_DEFAULT_LANGUAGE,
  LANGUAGE_CODE_RO,
  LANGUAGE_CODE_EN,
  LANGUAGE_CODE_RU,
} from '../../constants/i18n.constant';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function ManualLoaderFactory (
  translate: TranslateService,
  location: Location,
  settings: LocalizeRouterSettings,
): ManualParserLoader {
  return new ManualParserLoader(
    translate,
    location,
    settings,
    [LANGUAGE_CODE_RO, LANGUAGE_CODE_EN, LANGUAGE_CODE_RU],
    'ROUTES.',
    '!',
  );
}

@Injectable({
  providedIn: 'root',
})
export class I18nService {
  private readonly languageKey = LOCALIZE_DEFAULT_LANGUAGE;
  private readonly defaultLanguage = environment.DEFAULT_LANGUAGE;
  private readonly change$ = new BehaviorSubject<string>(null);
  private readonly languages = [
    {
      code: LANGUAGE_CODE_RO,
      text: '',
    },
    {
      code: LANGUAGE_CODE_EN,
      text: '',
    },
    {
      code: LANGUAGE_CODE_RU,
      text: '',
    },
  ];
  private readonly browserLanguage: string;
  constructor(
    private translate: TranslateService,
    private localizeService: LocalizeRouterService,
    private cookieService: CookieService,
  ) {
    this.browserLanguage = translate.getBrowserLang();
    translate.addLangs([LANGUAGE_CODE_RO, LANGUAGE_CODE_EN, LANGUAGE_CODE_RU]);
    this.initializeAppTranslation();
  }

  get change(): Observable<string> {
    return this.change$.asObservable().pipe(filter(w => w != null));
  }

  get currentLanguage(): string {
    return this.translate.currentLang || this.translate.getDefaultLang() || this.defaultLanguage;
  }

  /**
   * Each time the page loads, initializes translation based on
   * the language received in the url, else loads the default language
   */
  initializeAppTranslation(): void {
    this.translateData();
    Promise.resolve().then(() => {
      const code = this.cookieService.get(this.languageKey);

      if (code) {
        this.languages.map(language => {
          if (code && language.code === code) {
            this.use(language.code);
          }
        });
        return;
      }

      this.use(this.defaultLanguage);
    });
  }

  use(code: string): void {
    code = code || this.translate.getDefaultLang();
    this.translate.use(code).subscribe(() => this.change$.next(code));
  }

  get(key: string): Observable<string> {
    return this.translate.get(key);
  }

  get langs(): any {
    return this.languages;
  }

  translateData(): void {
    const requests = [];
    this.languages.map(l => requests.push(this.translate.get(`languages.${l.code}`)));

    forkJoin(requests).subscribe((response: string[]) => {
      response.map((text: string, index: number) => {
        this.languages[index].text = text;
      });
    });
  }
}