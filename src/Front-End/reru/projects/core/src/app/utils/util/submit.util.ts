import { Component, HostListener } from '@angular/core';

@Component({ template: '' })
export abstract class EnterSubmitListener {
    protected callback: Function;

    @HostListener('document:keyup.enter') onSubmit = () => this.submitData();

    private submitData(): void {
        if (this.callback && typeof this.callback === 'function') {
            this.callback();
        }
    }
}
