import { Component, Input, OnInit } from '@angular/core';
import { PermissionCheckerService } from '@erp/shared';
import { ArticleModel } from '../../../../utils/models/article.model';

@Component({
  selector: 'app-faq-name',
  templateUrl: './faq-name.component.html',
  styleUrls: ['./faq-name.component.scss']
})
export class FaqNameComponent implements OnInit {
  @Input() article: ArticleModel;
  permission:boolean = false;

  constructor(public permissionService: PermissionCheckerService) { }

  ngOnInit(): void {
    // if(this.permissionService.isGranted('P03010903')) 
      this.permission = true;
  }
}
