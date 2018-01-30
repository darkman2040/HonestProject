import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-time-widget',
  templateUrl: './user-time-widget.component.html',
  styleUrls: ['./user-time-widget.component.css']
})
export class UserTimeWidgetComponent implements OnInit {

  pieChartLabels:string[] = ['Win 10 nonsense', 'Word Converter', 'Management induced delays','Bill Backs'];
  pieChartData:number[] = [10, 30, 20, 40];
  pieChartType:string = 'pie';

  @Input() days:number;

  constructor() { }

  ngOnInit() {
  }

}
