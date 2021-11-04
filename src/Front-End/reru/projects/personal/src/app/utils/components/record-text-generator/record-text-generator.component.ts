import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-record-text-generator',
  templateUrl: './record-text-generator.component.html',
  styleUrls: ['./record-text-generator.component.scss']
})
export class RecordTextGeneratorComponent{
  @Input() text: string;
  @Input() type: number;
}
