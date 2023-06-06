import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaginationModel } from '../../../utils/models/pagination.model';

@Component({
  selector: 'app-required-documents-list',
  templateUrl: './required-documents-list.component.html',
  styleUrls: ['./required-documents-list.component.scss']
})
export class RequiredDocumentsListComponent implements OnInit {
	title: string;

  pagedSummary: PaginationModel = new PaginationModel();
  mobileButtonLength: string = "100%";
  
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  add() {
		this.router.navigate(['required-documents/add-document']);
	}

}
