import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-view-position-diagram-modal',
  templateUrl: './view-position-diagram-modal.component.html',
  styleUrls: ['./view-position-diagram-modal.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class ViewPositionDiagramModalComponent implements OnInit {

  eventsDiagram = [];
  usersDiagram = [];
  testTemplates = [];

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }
}
