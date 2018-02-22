import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegisterComponent } from './site-register.component';

describe('SiteRegisterComponent', () => {
  let component: SiteRegisterComponent;
  let fixture: ComponentFixture<SiteRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
