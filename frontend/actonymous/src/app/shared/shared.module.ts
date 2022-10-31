import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConnectionPingerModule } from './components/connection-pinger';

const COMPONENTS_MODULES = [ConnectionPingerModule];

@NgModule({
  declarations: [],
  exports: [COMPONENTS_MODULES],
  imports: [CommonModule, COMPONENTS_MODULES],
})
export class SharedModule {}
