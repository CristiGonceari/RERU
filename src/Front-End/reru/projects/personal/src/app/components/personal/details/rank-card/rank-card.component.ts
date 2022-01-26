import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RankModel } from 'projects/personal/src/app/utils/models/rank.model';

@Component({
  selector: 'app-rank-card',
  templateUrl: './rank-card.component.html',
  styleUrls: ['./rank-card.component.scss']
})
export class RankCardComponent {
  @Input() rank: RankModel;
  @Output() edit: EventEmitter<number> = new EventEmitter<number>();
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();
}
