import { Component, OnInit } from '@angular/core';
import { AdresService } from '../services/adres.service';
import { Adres } from './adres.model';

@Component({
  selector: 'app-adres',
  templateUrl: './adres.component.html',
  styleUrls: ['./adres.component.scss']
})
export class AdresComponent implements OnInit {
  adres: Adres;

  constructor(private adresService: AdresService) { }

  ngOnInit(): void {
    this.adresService.getAdres(1).subscribe(result => this.adres = result);
  }

}
