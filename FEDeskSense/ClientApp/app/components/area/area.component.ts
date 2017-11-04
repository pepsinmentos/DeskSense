import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'area',
    templateUrl: './area.component.html'
})
export class AreaComponent {
    public areas: Area[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Area/AllAreas').subscribe(result => {
            this.areas = result.json() as Area[];
        }, error => console.error(error));
    }
}

interface Area {
    id: number;
    name: string;
    parent: number;
}
