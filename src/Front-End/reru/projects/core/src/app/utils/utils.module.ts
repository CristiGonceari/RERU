import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';

const commonComponents = [
  LoadingSpinnerComponent,
];

@NgModule({
  declarations: commonComponents,
  imports: [CommonModule, NgbModule, TranslateModule, HttpClientModule],
  exports: [...commonComponents],
})
export class UtilsModule { }
