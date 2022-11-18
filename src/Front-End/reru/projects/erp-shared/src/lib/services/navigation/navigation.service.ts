import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
    private history: string[] = []

    constructor(private router: Router) { }

    public startSaveHistory() {
        this.router.events.subscribe((event) => {

            if (event instanceof NavigationEnd) {
                if (event.urlAfterRedirects.includes("token") == false){
                    this.history.push(event.urlAfterRedirects)
                }
            }
        })
    }

    public getHistory(): string[] {
        return this.history;
    }

    public goBack(): void {
        this.history.pop();

        if (this.history.length > 0) {
            this.router.navigateByUrl(this.history[this.history.length-1]);
            this.history.pop();
        } else {
            this.router.navigateByUrl("/")
        }
    }
    
    public getPreviousUrl(): string {
        if (this.history.length > 0) {
            const value =  this.history[this.history.length - 2];

            if (value == "/"){
                return 'Home'
            } else {
                if (value != null){
                    const result =  this.hasInt(value)

                    return result
                }
            }
        }

        return '';
    }

    private hasInt(me) {
        let i = 1,
        a = me.split(""),
        b = "",
        c = "";

        a.forEach(function(e){
         if (!isNaN(e)){
           c += e
           i++
         } else {b += e}
        })

        b = b.replace('//', '-');
        b = b.replace('/', '');
        b = b.replace('/', '-');

        return b;
      }
}