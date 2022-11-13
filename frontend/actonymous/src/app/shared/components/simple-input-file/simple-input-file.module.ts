import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiErrorModule } from '@taiga-ui/core';
import { TuiFieldErrorPipeModule, TuiInputFilesModule } from '@taiga-ui/kit';
import { SimpleInputFileComponent } from './components';

const COMPONENTS = [SimpleInputFileComponent];

@NgModule({
  declarations: [COMPONENTS],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TuiInputFilesModule,
    TuiErrorModule,
    TuiFieldErrorPipeModule,
  ],
  exports: [SimpleInputFileComponent],
})
export class SimpleInputFileModule {}
