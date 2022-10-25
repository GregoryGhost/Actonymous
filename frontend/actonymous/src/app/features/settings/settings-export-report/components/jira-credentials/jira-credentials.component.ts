import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'jira-credentials',
  templateUrl: './jira-credentials.component.html',
  styleUrls: ['./jira-credentials.component.scss']
})
export class JiraCredentialsComponent implements OnInit {

  public isPingingJiraServer: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  public pingJiraServer(event: MouseEvent): void {
    //TODO: raise event to ping jira server
  }

}
