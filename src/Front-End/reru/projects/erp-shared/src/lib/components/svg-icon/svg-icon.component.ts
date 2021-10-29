import { Component, ComponentFactoryResolver, Input, OnInit } from '@angular/core';
import icons from '../../assets/icons.json';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';

@Component({
	selector: 'svg-icon',
	templateUrl: './svg-icon.component.html',
	styleUrls: ['./svg-icon.component.scss'],
	providers: [SafeHtmlPipe],
})
export class SvgIconComponent implements OnInit {
	@Input() key: string;
	@Input() height: number = 50;
	@Input() width: number = 50;
	isLoading: boolean = false;
	path: string;

	ngOnInit(): void {
		this.render();
	}

	render(): void {
		let icon = this.key ? this.key : 'blocks';

		const el: any = icons.find((el: any) => el.name === icon);
		this.path = el.path;
		console.log(el.path);
		this.isLoading = false;
	}
}
