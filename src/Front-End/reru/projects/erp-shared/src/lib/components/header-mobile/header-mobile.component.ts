import { Component, Input, OnInit, Renderer2 } from '@angular/core';
import { SidebarService } from '../../services/sidebar.service';
import { SidebarView } from '../../models/sidebar.model';

@Component({
  selector: 'app-header-mobile',
  templateUrl: './header-mobile.component.html',
  styleUrls: ['./header-mobile.component.scss']
})
export class HeaderMobileComponent implements OnInit {
  @Input() logo: string;
  @Input() appLogo: string;
  @Input() disableSidenav: boolean;
  isOpenUserIcon: boolean;
  isOpenModules: boolean;
  isOpenUser: boolean;
  sidebarView = SidebarView;
  constructor(private sidebarService: SidebarService,
    private renderer: Renderer2) { }

  ngOnInit(): void {
    this.subscribeForSidebarChanges();
  }

  subscribeForSidebarChanges(): void {
    this.sidebarService.modules$.subscribe((response: boolean) => this.isOpenModules = response);
    this.sidebarService.user$.subscribe((response: boolean) => this.isOpenUser = response);
  }

  toggleUserIcon() {
    this.isOpenUserIcon = !this.isOpenUserIcon;
    if (this.isOpenUserIcon) {
      this.renderer.addClass(document.body, 'topbar-mobile-on');
    } else {
      this.renderer.removeClass(document.body, 'topbar-mobile-on');
    }
  }

  toggle(view: SidebarView) {
    this.sidebarService.toggle(view);
  }
}
