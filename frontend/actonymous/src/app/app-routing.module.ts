import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'report-generation',
    loadChildren: () =>
      import(
        './features/report/report-generation'
      ).then((m) => m.ReportGenerationModule),
  },
  {
    path: '',
    redirectTo: 'report-generation',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
