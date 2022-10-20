import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JiraCredentialsComponent } from './jira-credentials.component';

describe('JiraCredentialsComponent', () => {
  let component: JiraCredentialsComponent;
  let fixture: ComponentFixture<JiraCredentialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JiraCredentialsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JiraCredentialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
