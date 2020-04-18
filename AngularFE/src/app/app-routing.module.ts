import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdresComponent } from './adres/adres.component';
import { AdresListComponent } from './adres-list/adres-list.component';

const routes: Routes = [
  {path: 'adres/:id', component: AdresComponent },
  {path: 'adres', component: AdresComponent },
  {path: 'adres-list', component: AdresListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
