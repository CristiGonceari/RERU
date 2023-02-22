import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateService } from '@ngx-translate/core';
import { ManualParserLoader, LocalizeRouterSettings } from '@gilsdav/ngx-translate-router';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable, forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';
import { LOCALIZE_DEFAULT_LANGUAGE, LANGUAGE_CODE_RO, LANGUAGE_CODE_EN, LANGUAGE_CODE_RU } from '../constants/i18n.constant';
import { environment } from 'projects/personal/src/environments/environment';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function ManualLoaderFactory(
  translate: TranslateService,
  location: Location,
  settings: LocalizeRouterSettings,
): ManualParserLoader {
  return new ManualParserLoader(
    translate,
    location,
    settings,
    [
      LANGUAGE_CODE_RO, 
      LANGUAGE_CODE_EN, 
      LANGUAGE_CODE_RU
    ],
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
  constructor(private translate: TranslateService,
              private cookieService: CookieService) {
    this.initializeAppTranslation();
  }

  get change(): Observable<string> {
    return this.change$.asObservable().pipe(filter(w => w != null));
  }

  get currentLanguage(): string {
    return this.translate.currentLang;
  }

  get langs(): any {
    return this.languages;
  }

  /**
   * Each time the page loads, initializes translation based on
   * the language received in the url, else loads the default language
   */
  initializeAppTranslation(): void {
    this.translateLanguagePrefixes()
      .then(() => this.resolveDefaultLanguage())
      .then((languageCode: string) => {

        if (languageCode) {
          this.use(languageCode, true);
          return;
        }

        return this.use(this.defaultLanguage, true);
    });
  }

  use(code: string, isInit: boolean = false): void {
    code = code || this.translate.getDefaultLang();
    if (!isInit) this.cookieService.set(this.languageKey, code);
    this.translate.use(code).subscribe(() => this.change$.next(code));
  }

  get(key: string): Observable<string> {
    return this.translate.get(key);
  }

  translateLanguagePrefixes(): Promise<boolean> {
    return new Promise((resolve, _) => {
      const requests = [];
      this.languages.map(l => requests.push(this.translate.get(`languages.${l.code}`)));
  
      forkJoin(requests).subscribe((response: string[]) => {
        response.map((text: string, index: number) => {
          this.languages[index].text = text;
          resolve(true);
        });
      });
    });
  }

  /**
   * Finds out which default language to use, the priority:
   * 1. GETs code FROM URL (from redirect example)
   * 2. Uses Cookie set by use before
   * 3. Resolves the null language in cases it's missing, then it's set the default one
   * 
   * @returns {Promise<string>} language code for e.g. en, ro, ru
   */
  resolveDefaultLanguage(): Promise<string> {
    const URL = window.location.hash;
    
    for(let i = 0; i < this.languages.length; i++) {
      if (URL.includes(`#/${this.languages[i].code}`)) {
        this.cookieService.delete(this.languageKey);
        this.cookieService.set(this.languageKey, this.languages[i].code);
        return Promise.resolve(this.languages[i].code);
      }
    }

    return Promise.resolve(this.cookieService.get(this.languageKey));
  }
}
