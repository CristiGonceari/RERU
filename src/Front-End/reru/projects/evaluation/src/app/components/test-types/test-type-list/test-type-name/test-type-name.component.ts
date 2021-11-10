import { Component, Input, OnInit } from '@angular/core';
import { TestType } from 'projects/evaluation/src/app/utils/models/test-types/test-type.model';

@Component({
  selector: 'app-test-type-name',
  templateUrl: './test-type-name.component.html',
  styleUrls: ['./test-type-name.component.scss']
})
export class TestTypeNameComponent implements OnInit {
  @Input() test: TestType;
  permission:boolean = false;

  constructor() { }

  ngOnInit(): void {
  }
}
