import { Component, OnInit } from '@angular/core';
import { ConnectionPingStatuses } from 'src/app/shared/components/connection-pinger/models';

@Component({
  selector: 'jira-credentials',
  templateUrl: './jira-credentials.component.html',
  styleUrls: ['./jira-credentials.component.scss']
})
export class JiraCredentialsComponent implements OnInit {

  public pingJiraConnectionStatus = ConnectionPingStatuses.Idle;

  constructor() { }

  ngOnInit(): void {
  }

  public pingJiraServer(event: MouseEvent): void {
    this.pingJiraConnectionStatus = ConnectionPingStatuses.Pending;
  }

}
