import { Component, OnInit } from '@angular/core';
import { ConnectionPingStatuses } from 'src/app/shared/components/connection-pinger/models';

@Component({
  selector: 'morpher',
  templateUrl: './morpher.component.html',
  styleUrls: ['./morpher.component.scss']
})
export class MorpherComponent implements OnInit {

  public pingConnection = ConnectionPingStatuses.Idle;

  constructor() { }

  ngOnInit(): void {
  }

  public pingMorpher(event: MouseEvent): void {
    this.pingConnection = ConnectionPingStatuses.Pending;
  }
}
