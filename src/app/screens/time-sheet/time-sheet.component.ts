import { Component, OnInit } from '@angular/core';
import {TimeEntry} from './timeEntry'
import {MatTableDataSource} from '@angular/material';

@Component({
  selector: 'app-time-sheet',
  templateUrl: './time-sheet.component.html',
  styleUrls: ['./time-sheet.component.css']
})
export class TimeSheetComponent implements OnInit {

  displayedColumns = ['name', 'timeInHours'];
  dataSource = new MatTableDataSource(entries);

  constructor() {

  }

  ngOnInit() {
    this.dataSource.data.push(new TimeEntry("Another Project", 2.5));
  }

}

const entries: TimeEntry[] = [
  {name: "Win 10", timeInHours: 3.5},
  {name: "Bill Baks", timeInHours: 2.5},
  {name: "Adv Search", timeInHours: 2}
]
