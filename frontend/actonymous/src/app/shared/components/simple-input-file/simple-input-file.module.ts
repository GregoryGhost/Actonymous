import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiInputFilesModule } from '@taiga-ui/kit';
import { SimpleInputFileComponent } from './components';

const COMPONENTS = [SimpleInputFileComponent];

@NgModule({
  declarations: [
    COMPONENTS
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TuiInputFilesModule
  ],
  exports: [
    SimpleInputFileComponent
  ]
})
export class SimpleInputFileModule { }
