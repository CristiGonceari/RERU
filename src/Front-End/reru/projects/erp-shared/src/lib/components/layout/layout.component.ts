import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IAppSettings } from '../../models/app-settings.model';
import { SidebarView } from '../../models/sidebar.model';
import { SidebarService } from '../../services/sidebar.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  @Input() config: IAppSettings;
  @Input() logo: string;
  @Input() title: string;
  @Input() letter: string;
  @Input() languages: any[];
  @Input() currentLanguage: string;
  @Input() sidebarItems: any;
  @Input() hasBackground: boolean;
  @Input() backgroundColor: string = '#F3F6F9';
  @Input() moduleId: number;
  @Input() isCustomHeader: boolean;
  @Input() user: any;
  @Output() changeLanguage: EventEmitter<any> = new EventEmitter<any>();
  @Output() navigate: EventEmitter<any> = new EventEmitter<any>();
  @Output() logout: EventEmitter<any> = new EventEmitter<any>();
  @Output() changePassword: EventEmitter<any> = new EventEmitter<any>();

  isCollapsed: boolean;
  sidebarView = SidebarView;
  constructor(private sidebarService: SidebarService) {
    this.isCollapsed = this.sidebarService.isSidebarOn();
  }

  ngOnInit(): void {
    this.subscribeForSidebarChanges();
  }

  subscribeForSidebarChanges(): void {
    this.sidebarService.sidebar$.subscribe(response => this.isCollapsed = response);
  }

  closeRightSidebar(): void {
    this.sidebarService.toggle(this.sidebarView.USER);
  }

}
