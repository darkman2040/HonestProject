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
  projects: string[] = ["Adv Search", "Bill Backs", "Win 10"];
  newEntry: TimeEntryForm = new TimeEntryForm("", "");

  constructor() {
  }

  ngOnInit() {
  }

  onSubmit() {
    let copy:TimeEntry[] = this.dataSource.data.slice();
    copy.push(new TimeEntry(this.newEntry.name, Number(this.newEntry.timeInString)));
    this.dataSource = new MatTableDataSource(copy); 
    this.newEntry.name = ''
    this.newEntry.timeInString = ''
  }

}

class TimeEntryForm{
  constructor(public name: string,
    public timeInString: string){}
  
}

const entries: TimeEntry[] = [
  {name: "Win 10", timeInHours: 3.5},
  {name: "Bill Backs", timeInHours: 2.5},
  {name: "Adv Search", timeInHours: 2}
]
