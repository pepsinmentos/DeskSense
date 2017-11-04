import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'occupancylive',
    templateUrl: './occupancylive.component.html'
})
export class OccupancyLiveComponent {
    public occupancyData: ChartData[];
    public lineChartLabels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
    public lineChartOptions: any = { responsive: true };
    public lineChartLegend: boolean = true;
    public lineChartType: string = "line";
    public lineChartColors: Array<any> = [
        { // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        },
        { // red
            backgroundColor: 'rgba(234,0,0,0.2)',
            borderColor: 'rgba(234,0,0,1)',
            pointBackgroundColor: 'rgba(234,0,0,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(234,0,0,1)'
        },
        { // blue
            backgroundColor: 'rgba(0, 117, 234,0.2)',
            borderColor: 'rgba(0, 117, 234,1)',
            pointBackgroundColor: 'rgba(0, 117, 234,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(0, 117, 234,0.8)'
        },
        { // orange
            backgroundColor: 'rgba(234, 117, 0,0.2)',
            borderColor: 'rgba(234, 117, 0,1)',
            pointBackgroundColor: 'rgba(234, 117, 0,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(234, 117, 0,0.8)'
        },
        { // light green
            backgroundColor: 'rgba(35, 199, 43,0.2)',
            borderColor: 'rgba(35, 199, 43,1)',
            pointBackgroundColor: 'rgba(35, 199, 43,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(35, 199, 43,0.8)'
        }
    ];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Area/OccupancyLive').subscribe(result => {
            this.occupancyData = result.json() as ChartData[];
        }, error => console.error(error));
    }
}

interface ChartData {
    data:Array<number>;
    label: string;
}
