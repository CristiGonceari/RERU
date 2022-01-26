import { Component, Input } from '@angular/core';
import { RankModel } from '../../../utils/models/rank.model';

@Component({
  selector: 'app-profile-ranks-card',
  templateUrl: './profile-ranks-card.component.html',
  styleUrls: ['./profile-ranks-card.component.scss']
})
export class ProfileRanksCardComponent {
  @Input() rank: RankModel;
}
