import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../components/svg-icon/svg-icon.component';
import { SafeHtmlPipe } from '../pipes/safe-html.pipe';
import { SharedPipesModule } from './shared-pipes.module';



@NgModule({
  declarations: [SvgIconComponent],
  imports: [CommonModule, SharedPipesModule],
  exports: [SvgIconComponent],
  providers: [SafeHtmlPipe]
})
export class SvgModule { }
