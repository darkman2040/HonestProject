import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagermentComponent } from './project-managerment.component';

describe('ProjectManagermentComponent', () => {
  let component: ProjectManagermentComponent;
  let fixture: ComponentFixture<ProjectManagermentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectManagermentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectManagermentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
