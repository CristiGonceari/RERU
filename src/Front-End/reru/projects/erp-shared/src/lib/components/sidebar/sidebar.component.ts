import { Component, OnInit, Output, EventEmitter, Input, ViewEncapsulation } from '@angular/core';
import { SidebarService } from '../../services/sidebar.service';
import { SidebarView, SidebarItemType } from '../../models/sidebar.model';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';
import { PermissionCheckerService } from '../../services/permission-checker.service';

@Component({
	selector: 'app-sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
	providers: [SafeHtmlPipe],
	encapsulation: ViewEncapsulation.None,
})
export class SidebarComponent implements OnInit {
	isOpen: boolean;
	sidebarView = SidebarView;
	sidebarItemType = SidebarItemType;
	isActiveTab: boolean[] = [true];
	sidebarHeight: string;
	@Input() title: string;
	@Input() menuItems: any[] = [];
	@Output() navigate: EventEmitter<number> = new EventEmitter<number>();
	constructor(private sidebarService: SidebarService, private permissionCheckerService: PermissionCheckerService) {}

	ngOnInit(): void {
		this.subscribeForSidebarChanges();
		if (!this.menuItems || !this.menuItems.length) {
			this.sidebarService.toggle(SidebarView.SIDEBAR, null);
		} 
	}

	subscribeForSidebarChanges(): void {
		this.sidebarService.sidebar$.subscribe(response => (this.isOpen = response));
	}

	navigateTo(index: number): void {
		if (!isNaN(index)) {
			this.isActiveTab = [];
			this.isActiveTab[index] = true;
			this.navigate.emit(index);
		}
	}

	toggle(view: SidebarView, index: number): void {
		this.sidebarService.toggle(view);
		if (!isNaN(index)) {
			this.navigate.emit(index);
		}
	}

	hasPermission(menuItem: any): boolean {
		if (!menuItem.permission) {
			return true;
		}
		return this.permissionCheckerService.isGranted(menuItem.permission);
	}

	hasPermissions(permissions: any): boolean {
		if (!permissions) {
			return true;
		}
		return this.permissionCheckerService.areGranted(permissions);
	}
}
