import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConnectionPingerComponent } from './components';
import { TuiButtonModule, TuiNotificationModule } from '@taiga-ui/core';
import { ReactiveFormsModule } from '@angular/forms';

const COMPONENTS = [ConnectionPingerComponent];

@NgModule({
  declarations: [COMPONENTS],
  exports: [ConnectionPingerComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TuiButtonModule,
    TuiNotificationModule,
  ],
})
export class ConnectionPingerModule {}
