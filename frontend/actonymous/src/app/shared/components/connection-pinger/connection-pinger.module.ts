import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConnectionPingerComponent } from './components';
import { TuiButtonModule, TuiNotificationModule } from '@taiga-ui/core';

const COMPONENTS = [ConnectionPingerComponent];

@NgModule({
  declarations: [COMPONENTS],
  exports: [ConnectionPingerComponent],
  imports: [CommonModule, TuiButtonModule, TuiNotificationModule],
})
export class ConnectionPingerModule {}
