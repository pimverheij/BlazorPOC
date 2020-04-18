import { Component, OnInit } from '@angular/core';
import { AdresService } from '../services/adres.service';
import { Adres } from '../models/adres.model';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-adres',
  templateUrl: './adres.component.html',
  styleUrls: ['./adres.component.scss']
})
export class AdresComponent implements OnInit {
  adres: Adres;
  submitButtonText: string;

  constructor(private adresService: AdresService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    if(id){
      this.adresService.getAdres(id).subscribe(result => this.adres = result);
      this.submitButtonText = 'Update adres';
    }
    else{
      this.adres = new Adres();
      this.submitButtonText = 'Voeg adres toe';
    }
  }

  submitAdres(form: NgForm) {
    if(!form.valid) return;

    if(this.adres.id)
      this.adresService.updateAdres(this.adres).subscribe(
        () => this.router.navigate(['adres-list'])
      );
    else
      this.adresService.addAdres(this.adres).subscribe(
        () => this.router.navigate(['adres-list'])
      );
  }

  getPostcodePattern() {
    return this.adres.isBinnenlandsAdres() ? '^[1-9][0-9]{3} ?[A-Za-z]{2}$' : '^[1-9][0-9]{4}';
  }
}
