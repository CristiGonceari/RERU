import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafeHtmlPipe } from '../pipes/safe-html.pipe';


@NgModule({
  declarations: [SafeHtmlPipe],
  imports: [CommonModule],
  providers: [SafeHtmlPipe],
  exports: [SafeHtmlPipe]
})
export class SharedPipesModule { }