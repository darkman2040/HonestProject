import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';

import {ScreensRoutingModule} from './screens.routing.module'


import {MaterialModule} from '../angularmaterials.module'


@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    ScreensRoutingModule
  ],
  declarations: [
  ],
  providers: [ ]
})
export class ScreensModule {}