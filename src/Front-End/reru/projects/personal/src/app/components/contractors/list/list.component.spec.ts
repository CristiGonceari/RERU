import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListComponent } from './list.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpLoaderFactory } from '../../../utils/services/i18n.service';
import { HttpClient } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../../utils/utils.module';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { ContractorService } from '../../../utils/services/contractor.service';
import { of } from 'rxjs';

describe('ListComponent', () => {
  let component: ListComponent;
  let fixture: ComponentFixture<ListComponent>;
  let contractorService: ContractorService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        SimpleNotificationsModule.forRoot(),
        HttpClientTestingModule,
        TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient],
          },
        }),
        NgbModule,
        UtilsModule
      ],
      declarations: [ListComponent]
    })
      .compileComponents();
    contractorService = TestBed.inject(ContractorService);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve list of contractors', () => {
    spyOn(contractorService, 'list').and.returnValue(of({ success: true, data: { items: [{}, {}], pagedSummary: { pageSize: 10, totalPages: 1, currentPage: 1, totalCount: 2 } } }));
    const spy = spyOn(component, 'list').and.callThrough();
    component.list();
    expect(spy).toHaveBeenCalled();
  })

  it('should retrieve list of contractors with items null', () => {
    spyOn(contractorService, 'list').and.returnValue(of({ success: true, data: { items: null, pagedSummary: { pageSize: 10, totalPages: 1, currentPage: 1, totalCount: 2 } } }));
    const spy = spyOn(component, 'list').and.callThrough();
    component.list();
    expect(spy).toHaveBeenCalled();
  })

  it('should retrieve list of contractors and fail on success', () => {
    spyOn(contractorService, 'list').and.returnValue(of({ success: false, data: null }));
    const spy = spyOn(component, 'list').and.callThrough();
    component.list();
    expect(spy).toHaveBeenCalled();
  })
});
