import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTimeWidgetComponent } from './user-time-widget.component';

describe('UserTimeWidgetComponent', () => {
  let component: UserTimeWidgetComponent;
  let fixture: ComponentFixture<UserTimeWidgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserTimeWidgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTimeWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
