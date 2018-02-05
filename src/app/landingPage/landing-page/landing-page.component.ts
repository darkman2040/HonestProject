import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import {MediaMatcher} from '@angular/cdk/layout';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  title = 'Honest Project';
  mobileQuery: MediaQueryList;

  fillerNav = [`Dashboard`, `Timesheet`];

  pieChartLabels:string[] = ['Win 10 nonsense', 'Word Converter', 'Management induced delays','Bill Backs'];
  pieChartData:number[] = [10, 30, 20, 40];
  pieChartType:string = 'pie';

  private _mobileQueryListener: () => void;

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  constructor(private _httpService: Http, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) { 
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }
   apiValues: string[] = [];
   
   ngOnInit() {
      this._httpService.get('/api/values').subscribe(values => {
         this.apiValues = values.json() as string[];
      });
   }

}
