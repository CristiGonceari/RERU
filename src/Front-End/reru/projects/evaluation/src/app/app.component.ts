import { Component, OnInit } from '@angular/core';
import { I18nService } from '../app/utils/services/i18n/i18n.service';
import { Router } from '@angular/router';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { forkJoin } from 'rxjs';
import { SidebarItemType } from '../app/utils/models/sidebar.model';
import { InternalGetTestIdService } from '../app/utils/services/internal-get-test-id/internal-get-test-id.service';
import { AppSettingsService, IAppSettings, AuthenticationService, NavigationService } from '@erp/shared';
import { Test } from './utils/models/tests/test.model';
import { NotificationsService } from 'angular2-notifications';
// import { IAppConfig } from '../../utils/models/app-config.model';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	type: string;
	messageText: string;
	testId: number;
	showMultipleQuestionsPerPega: boolean;

	options: {
		animate: 'fromTop';
		position: ['top', 'right'];
		timeOut: 2000;
		lastOnBottom: true;
		showProgressBar: true;
	};


	sidebarItems: any[] = [
		{
			type: SidebarItemType.ITEM,
			url: '/',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<rect x="0" y="0" width="24" height="24"/>
				<path d="M3.95709826,8.41510662 L11.47855,3.81866389 C11.7986624,3.62303967 12.2013376,3.62303967 12.52145,3.81866389 L20.0429,8.41510557 C20.6374094,8.77841684 21,9.42493654 21,10.1216692 L21,19.0000642 C21,20.1046337 20.1045695,21.0000642 19,21.0000642 L4.99998155,21.0000673 C3.89541205,21.0000673 2.99998155,20.1046368 2.99998155,19.0000673 L2.99999828,10.1216672 C2.99999935,9.42493561 3.36258984,8.77841732 3.95709826,8.41510662 Z M10,13 C9.44771525,13 9,13.4477153 9,14 L9,17 C9,17.5522847 9.44771525,18 10,18 L14,18 C14.5522847,18 15,17.5522847 15,17 L15,14 C15,13.4477153 14.5522847,13 14,13 L10,13 Z" fill="#000000"/>
			</g>
		</svg>`,
		},
		{
			type: SidebarItemType.ITEM,
			url: '/my-activities',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <polygon points="0 0 24 0 24 24 0 24"/>
        <path d="M12,11 C9.790861,11 8,9.209139 8,7 C8,4.790861 9.790861,3 12,3 C14.209139,3 16,4.790861 16,7 C16,9.209139 14.209139,11 12,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"/>
        <path d="M3.00065168,20.1992055 C3.38825852,15.4265159 7.26191235,13 11.9833413,13 C16.7712164,13 20.7048837,15.2931929 20.9979143,20.2 C21.0095879,20.3954741 20.9979143,21 20.2466999,21 C16.541124,21 11.0347247,21 3.72750223,21 C3.47671215,21 2.97953825,20.45918 3.00065168,20.1992055 Z" fill="#000000" fill-rule="nonzero"/>
    </g>
</svg>`,
		},
		{
			permissions: ['P03000401', 'P03000501', 'P03000801'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03000401',
			type: SidebarItemType.ITEM,
			url: '/categories',
			name: '',
			icon: `<svg _ngcontent-jkr-c110="" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"
        xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
          <rect x="0" y="0" width="24" height="24"></rect>
          <rect fill="#000000" x="4" y="4" width="8" height="16" opacity="0.5"></rect>
          <path d="M6,18 L9,18 C9.66666667,18.1143819 10,18.4477153 10,19 C10,19.5522847 9.66666667,19.8856181 9,20 L4,
          20 L4,15 C4,14.3333333 4.33333333,14 5,14 C5.66666667,14 6,14.3333333 6,15 L6,18 Z M18,18 L18,15 C18.1143819,
          14.3333333 18.4477153,14 19,14 C19.5522847,14 19.8856181,14.3333333 20,15 L20,20 L15,20 C14.3333333,20 14,
          19.6666667 14,19 C14,18.3333333 14.3333333,18 15,18 L18,18 Z M18,6 L15,6 C14.3333333,5.88561808 14,5.55228475
          14,5 C14,4.44771525 14.3333333,4.11438192 15,4 L20,4 L20,9 C20,9.66666667 19.6666667,10 19,10 C18.3333333,
          10 18,9.66666667 18,9 L18,6 Z M6,6 L6,9 C5.88561808,9.66666667 5.55228475,10 5,10 C4.44771525,10 4.11438192,
          9.66666667 4,9 L4,4 L9,4 C9.66666667,4 10,4.33333333 10,5 C10,5.66666667 9.66666667,6 9,6 L6,6 Z"
          fill="#000000"
          fill-rule="nonzero"
          </path>
        </g>
      </svg>`,
		},
		{
			permission: 'P03000501',
			type: SidebarItemType.ITEM,
			url: '/questions',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
        width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
          <rect x="0" y="0" width="24" height="24" />
          <path d="M7,3 L17,3 C19.209139,3 21,4.790861 21,7 C21,9.209139 19.209139,11 17,11 L7,11 C4.790861,11 3,
          9.209139 3,7 C3,4.790861 4.790861,3 7,3 Z M7,9 C8.1045695,9 9,8.1045695 9,7 C9,5.8954305 8.1045695,5 7,
          5 C5.8954305,5 5,5.8954305 5,7 C5,8.1045695 5.8954305,9 7,9 Z" fill="#000000" />
          <path d="M7,13 L17,13 C19.209139,13 21,14.790861 21,17 C21,19.209139 19.209139,21 17,21 L7,21 C4.790861,
          21 3,19.209139 3,17 C3,14.790861 4.790861,13 7,13 Z M17,19 C18.1045695,19 19,18.1045695 19,17 C19,
          15.8954305 18.1045695,15 17,15 C15.8954305,15 15,15.8954305 15,17 C15,18.1045695 15.8954305,19 17,19 Z"
          fill="#000000" opacity="0.3" />
        </g>
      </svg>`,
		},
		{
			permission: 'P03000801',
			type: SidebarItemType.ITEM,
			url: '/test-type',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
        width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
          <rect x="0" y="0" width="24" height="24"></rect>
          <rect fill="#000000" x="4" y="4" width="7" height="7" rx="1.5"></rect>
          <path d="M5.5,13 L9.5,13 C10.3284271,13 11,13.6715729 11,14.5 L11,18.5 C11,19.3284271 10.3284271,20 9.5,
            20 L5.5,20 C4.67157288,20 4,19.3284271 4,18.5 L4,14.5 C4,13.6715729 4.67157288,13 5.5,13 Z M14.5,4 L18.5,
            4 C19.3284271,4 20,4.67157288 20,5.5 L20,9.5 C20,10.3284271 19.3284271,11 18.5,11 L14.5,11 C13.6715729,
            11 13,10.3284271 13,9.5 L13,5.5 C13,4.67157288 13.6715729,4 14.5,4 Z M14.5,13 L18.5,13 C19.3284271,13 20,
            13.6715729 20,14.5 L20,18.5 C20,19.3284271 19.3284271,20 18.5,20 L14.5,20 C13.6715729,20 13,19.3284271 13,
            18.5 L13,14.5 C13,13.6715729 13.6715729,13 14.5,13 Z"
            fill="#000000" opacity="0.3">
          </path>
        </g>
      </svg>`,
		},
		{
			permissions: ['P03001101', 'P03000601', 'P03000901'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03001101',
			type: SidebarItemType.ITEM,
			url: '/solicited-tests',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<rect x="0" y="0" width="24" height="24"/>
				<path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953) "/>
				<path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"/>
			</g>
		</svg>`,
		},
		{
			permission: 'P03000601',
			type: SidebarItemType.ITEM,
			url: '/tests',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px"
        height="24px" viewBox="0 0 24 24" version="1.1">
        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <path d="M5,3 L6,3 C6.55228475,3 7,3.44771525 7,4 L7,20 C7,20.5522847 6.55228475,21 6,21 L5,21 C4.44771525,21 4,
        20.5522847 4,20 L4,4 C4,3.44771525 4.44771525,3 5,3 Z M10,3 L11,3 C11.5522847,3 12,3.44771525 12,4 L12,20 C12,
        20.5522847 11.5522847,21 11,21 L10,21 C9.44771525,21 9,20.5522847 9,20 L9,4 C9,3.44771525 9.44771525,3 10,3 Z"
        fill="#000000"/>
        <rect fill="#000000" opacity="0.3" transform="translate(17.825568, 11.945519) rotate(-19.000000)
        translate(-17.825568, -11.945519) " x="16.3255682" y="2.94551858" width="3" height="18" rx="1"/>
        </g>
      </svg>`,
		},
		{
			permission: 'P03000601',
			type: SidebarItemType.ITEM,
			url: '/evaluations',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
			width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
			  <rect x="0" y="0" width="24" height="24" />
			  <path d="M7,3 L17,3 C19.209139,3 21,4.790861 21,7 C21,9.209139 19.209139,11 17,11 L7,11 C4.790861,11 3,
			  9.209139 3,7 C3,4.790861 4.790861,3 7,3 Z M7,9 C8.1045695,9 9,8.1045695 9,7 C9,5.8954305 8.1045695,5 7,
			  5 C5.8954305,5 5,5.8954305 5,7 C5,8.1045695 5.8954305,9 7,9 Z" fill="#000000" />
			  <path d="M7,13 L17,13 C19.209139,13 21,14.790861 21,17 C21,19.209139 19.209139,21 17,21 L7,21 C4.790861,
			  21 3,19.209139 3,17 C3,14.790861 4.790861,13 7,13 Z M17,19 C18.1045695,19 19,18.1045695 19,17 C19,
			  15.8954305 18.1045695,15 17,15 C15.8954305,15 15,15.8954305 15,17 C15,18.1045695 15.8954305,19 17,19 Z"
			  fill="#000000" opacity="0.3" />
			</g>
		  </svg>`,
		},
		{
			permission: 'P03000901',
			type: SidebarItemType.ITEM,
			url: '/statistics',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<rect x="0" y="0" width="24" height="24"/>
				<rect fill="#000000" opacity="0.3" x="17" y="4" width="3" height="13" rx="1.5"/>
				<rect fill="#000000" opacity="0.3" x="12" y="9" width="3" height="8" rx="1.5"/>
				<path d="M5,19 L20,19 C20.5522847,19 21,19.4477153 21,20 C21,20.5522847 20.5522847,21 20,21 L4,21 C3.44771525,21 3,20.5522847 3,20 L3,4 C3,3.44771525 3.44771525,3 4,3 C4.55228475,3 5,3.44771525 5,4 L5,19 Z" fill="#000000" fill-rule="nonzero"/>
				<rect fill="#000000" opacity="0.3" x="7" y="11" width="3" height="6" rx="1.5"/>
			</g>
		</svg>`,
		},
		{
			permissions: ['P03000201', 'P03000101', 'P03000301'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03000201',
			type: SidebarItemType.ITEM,
			url: '/locations',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
				<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
					<rect x="0" y="0" width="24" height="24"/>
					<path d="M5,10.5 C5,6 8,3 12.5,3 C17,3 20,6.75 20,10.5 C20,12.8325623 17.8236613,16.03566 13.470984,20.1092932 C12.9154018,20.6292577 12.0585054,20.6508331 11.4774555,20.1594925 C7.15915182,16.5078313 5,13.2880005 5,10.5 Z M12.5,12 C13.8807119,12 15,10.8807119 15,9.5 C15,8.11928813 13.8807119,7 12.5,7 C11.1192881,7 10,8.11928813 10,9.5 C10,10.8807119 11.1192881,12 12.5,12 Z" fill="#000000" fill-rule="nonzero"/>
				</g>
			</svg>`,
		},
		{
			permission: 'P03000101',
			type: SidebarItemType.ITEM,
			url: '/events',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
				<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
					<rect x="0" y="0" width="24" height="24"/>
					<path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953) "/>
					<path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"/>
				</g>
			</svg>`,
		},
		{
			permission: 'P03000301',
			type: SidebarItemType.ITEM,
			url: '/plans',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<rect x="0" y="0" width="24" height="24"/>
				<path d="M8,3 L8,3.5 C8,4.32842712 8.67157288,5 9.5,5 L14.5,5 C15.3284271,5 16,4.32842712 16,3.5 L16,3 L18,3 C19.1045695,3 20,3.8954305 20,5 L20,21 C20,22.1045695 19.1045695,23 18,23 L6,23 C4.8954305,23 4,22.1045695 4,21 L4,5 C4,3.8954305 4.8954305,3 6,3 L8,3 Z" fill="#000000" opacity="0.3"/>
				<path d="M11,2 C11,1.44771525 11.4477153,1 12,1 C12.5522847,1 13,1.44771525 13,2 L14.5,2 C14.7761424,2 15,2.22385763 15,2.5 L15,3.5 C15,3.77614237 14.7761424,4 14.5,4 L9.5,4 C9.22385763,4 9,3.77614237 9,3.5 L9,2.5 C9,2.22385763 9.22385763,2 9.5,2 L11,2 Z" fill="#000000"/>
				<rect fill="#000000" opacity="0.3" x="10" y="9" width="7" height="2" rx="1"/>
				<rect fill="#000000" opacity="0.3" x="7" y="9" width="2" height="2" rx="1"/>
				<rect fill="#000000" opacity="0.3" x="7" y="13" width="2" height="2" rx="1"/>
				<rect fill="#000000" opacity="0.3" x="10" y="13" width="7" height="2" rx="1"/>
				<rect fill="#000000" opacity="0.3" x="7" y="17" width="2" height="2" rx="1"/>
				<rect fill="#000000" opacity="0.3" x="10" y="17" width="7" height="2" rx="1"/>
			</g>
		</svg>`,
		},
		{
			permissions: ['P03001301'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03001301',
			type: SidebarItemType.ITEM,
			url: '/documents-templates',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<polygon points="0 0 24 0 24 24 0 24"/>
				<path d="M5.85714286,2 L13.7364114,2 C14.0910962,2 14.4343066,2.12568431 14.7051108,2.35473959 L19.4686994,6.3839416 C19.8056532,6.66894833 20,7.08787823 20,7.52920201 L20,20.0833333 C20,21.8738751 19.9795521,22 18.1428571,22 L5.85714286,22 C4.02044787,22 4,21.8738751 4,20.0833333 L4,3.91666667 C4,2.12612489 4.02044787,2 5.85714286,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"/>
				<rect fill="#000000" x="6" y="11" width="9" height="2" rx="1"/>
				<rect fill="#000000" x="6" y="15" width="5" height="2" rx="1"/>
			</g>
		</svg>`,
		},
		{
			permissions: ['P03001201', 'P03001401'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03001201',
			type: SidebarItemType.ITEM,
			url: '/positions',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
				<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
					<rect x="0" y="0" width="24" height="24"/>
					<path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953) "/>
					<path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"/>
				</g>
			</svg>`,
		},
		{
			permission: 'P03001401',
			type: SidebarItemType.ITEM,
			url: '/required-documents',
			name: '',
			icon: `<i class="fa far fa-file-alt icon-lg"></i>`,
		},
		{
			permissions: ['P03000001'],
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P03000001',
			type: SidebarItemType.ITEM,
			url: '/faq',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px"
            viewBox="0 0 24 24" version="1.1">
            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
            <rect x="0" y="0" width="24" height="24"/>
            <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10"/>
            <path d="M12,16 C12.5522847,16 13,16.4477153 13,17 C13,17.5522847 12.5522847,18 12,18 C11.4477153,18 11,17.5522847 11,17 C11,16.4477153
            11.4477153,16 12,16 Z M10.591,14.868 L10.591,13.209 L11.851,13.209 C13.447,13.209 14.602,11.991 14.602,10.395 C14.602,8.799 13.447,7.581
            11.851,7.581 C10.234,7.581 9.121,8.799 9.121,10.395 L7.336,10.395 C7.336,7.875 9.31,5.922 11.851,5.922 C14.392,5.922 16.387,7.875 16.387,10.395
            C16.387,12.915 14.392,14.868 11.851,14.868 L10.591,14.868 Z" fill="#000000"/>
            </g>
        </svg>`,
		},
	];
	appSettings: IAppSettings;
	constructor(
		private router: Router,
		public translate: I18nService,
		private localize: LocalizeRouterService,
		private appSettingsService: AppSettingsService,
		private authenticationService: AuthenticationService,
		public navigation: NavigationService,
		private internalGetTest: InternalGetTestIdService,
		public notificationService: NotificationsService,

	) {
		this.appSettings = this.appSettingsService.settings;
		this.navigation.startSaveHistory();
	}

	ngOnInit(): void {
		this.translateData();
		this.translate.change.subscribe(() => this.translateData());
		this.setIntrvl();
		this.getTestId();
	}

	navigate(index: number): void {
		this.sidebarItems[index] && this.router.navigate([this.localize.translateRoute(this.sidebarItems[index].url)]);
	}

	translateData(): void {
		forkJoin([
			this.translate.get('sidebar.home'),
			this.translate.get('dashboard.my-activities'),
			this.translate.get('sidebar.settings'),
			this.translate.get('sidebar.manage-categories'),
			this.translate.get('sidebar.manage-questions'),
			this.translate.get('sidebar.manage-tests'),
			this.translate.get('sidebar.test'),
			this.translate.get('solicited-position.solicited-positions'),
			this.translate.get('verify-test.title'),
			this.translate.get('evaluations.evaluations'),
			this.translate.get('statistics.statistics'),
			this.translate.get('sidebar.events'),
			this.translate.get('locations.locations'),
			this.translate.get('events.events'),
			this.translate.get('plans.plans'),
			this.translate.get('sidebar.documents-management'),
			this.translate.get('sidebar.documents-template'),
			this.translate.get('sidebar.administration'),
			this.translate.get('sidebar.positions'),
			this.translate.get('sidebar.required-documents'),
			this.translate.get('faq.help'),
			this.translate.get('faq.faq'),
		]).subscribe(([home, activities, settings, categories, questions, tests, test, solicitedTest, verifyTest, evaluations, statistic, event, location, events, plan, documentsManagement, documentsTemplate, administration, position, requiredDocuments, help, faq]) => {
			this.sidebarItems[0].name = home;
			this.sidebarItems[1].name = activities;
			this.sidebarItems[2].name = settings;
			this.sidebarItems[3].name = categories;
			this.sidebarItems[4].name = questions;
			this.sidebarItems[5].name = tests;
			this.sidebarItems[6].name = test;
			this.sidebarItems[7].name = solicitedTest;
			this.sidebarItems[8].name = verifyTest;
			this.sidebarItems[9].name = evaluations;
			this.sidebarItems[10].name = statistic;
			this.sidebarItems[11].name = event;
			this.sidebarItems[12].name = location;
			this.sidebarItems[13].name = events;
			this.sidebarItems[14].name = plan;
			this.sidebarItems[15].name = documentsManagement;
			this.sidebarItems[16].name = documentsTemplate;
			this.sidebarItems[17].name = administration;
			this.sidebarItems[18].name = position;
			this.sidebarItems[19].name = requiredDocuments;
			this.sidebarItems[20].name = help;
			this.sidebarItems[21].name = faq;
		});
	}

	setIntrvl() {
		setInterval(() => this.getTestId(), 360_000);
	}

	getTestId() {
		this.internalGetTest.getTestIdForFastStart().subscribe(() => {});
	}

	logout() {
		this.authenticationService.signout();
	}
}
