import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdresComponent } from './adres/adres.component';


const routes: Routes = [
  {path: 'adres', component: AdresComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
