import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material';

@Component({
  selector: 'app-project-viewer',
  templateUrl: './project-viewer.component.html',
  styleUrls: ['./project-viewer.component.css']
})
export class ProjectViewerComponent implements OnInit {

  displayedColumns = ['name', 'projectedDate'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  constructor() { }

  ngOnInit() {
  }

}

export interface Element {
  name: string;
  projectedDate: string;
}

const ELEMENT_DATA: Element[] = [
  {name: 'Word Converter', projectedDate: 'Aug. 18, 2018'},
  {name: 'Bill Backs', projectedDate: 'Mar. 15, 2018'},
  {name: 'Adv. Search Web', projectedDate: 'Lolz'},
];
