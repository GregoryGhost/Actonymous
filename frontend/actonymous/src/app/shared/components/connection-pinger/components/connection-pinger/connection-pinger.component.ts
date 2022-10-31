import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ConnectionPingStatuses } from '../../models';

@Component({
  selector: 'connection-pinger',
  templateUrl: './connection-pinger.component.html',
  styleUrls: ['./connection-pinger.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConnectionPingerComponent implements OnInit, OnChanges {

  @Output()
  public onClick = new EventEmitter<MouseEvent>();

  @Input()
  public status = ConnectionPingStatuses.Idle;

  public statuses = ConnectionPingStatuses;

  public isPendingStatus = false;

  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    const actualStatus = (changes['status'].currentValue as unknown as ConnectionPingStatuses);
    this.isPendingStatus = actualStatus === ConnectionPingStatuses.Pending;
  }

  public ping(event: MouseEvent): void {
    this.onClick.emit(event);
  }
}
