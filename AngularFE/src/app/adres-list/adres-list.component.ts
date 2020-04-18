import { Component, OnInit, ViewChild } from '@angular/core';
import { AdresService } from '../services/adres.service';
import { Adres } from '../models/adres.model';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-adres-list',
  templateUrl: './adres-list.component.html',
  styleUrls: ['./adres-list.component.scss']
})
export class AdresListComponent implements OnInit {
  adressen: Adres[];
  displayedColumns: string[] = ['id', 'straat', 'postcode', 'gemeente', 'land', 'actions'];
  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private adresService: AdresService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.adresService.getAdressen().subscribe(result => this.adressen = result);
  }

  wijzigAdres(adresId: number){
    this.router.navigate(['adres', adresId]);
  }

  verwijderAdres(adresId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: {title: `Weet u zeker dat u het adres wilt verwijderen?`}
    });

     dialogRef.afterClosed().subscribe(dialogResult => {
       if(dialogResult){
        this.adresService.deleteAdres(adresId).subscribe(
          result => {
            var index = this.adressen.findIndex(x => x.id === adresId);
            if (index > -1){
              this.adressen.splice(index, 1);
              this.table.renderRows();
            }
          }
        );
       }
    });
  }
}
