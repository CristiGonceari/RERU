import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { PermissionCheckerService } from '../services/permission-checker.service';
import { ApplicationUserService } from '../services/application-user.service';

@Directive({
	selector: '[permission]',
})
export class PermissionDirective {
	@Input()
	set permission(permission: string) {
		this.permissionService.isGranted(permission)
			? this.container.createEmbeddedView(this.templateRef)
			: this.container.clear();
	}
	constructor(
		private templateRef: TemplateRef<any>,
		private container: ViewContainerRef,
		private permissionService: PermissionCheckerService
	) {}
}
