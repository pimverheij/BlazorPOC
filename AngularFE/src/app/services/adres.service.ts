import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Adres } from '../models/adres.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdresService {

  private baseUrl: string = 'http://localhost:65157/adres';
  //private baseUrl: string = 'https://backendvalidaties.azurewebsites.net/adres';
  public getAdres(id: number) : Observable<Adres> {
    return this.http.get<Adres>(`${this.baseUrl}/${id}`)
            .pipe(map(data => new Adres().deserialize(data)));
  }

  public getAdressen() : Observable<Adres[]> {
    return this.http.get<Adres[]>(`${this.baseUrl}`)
            .pipe(map(data => data.map(data => new Adres().deserialize(data))));
  }

  public addAdres(adres: Adres) : Observable<Adres>{
    return this.http.post<Adres>(`${this.baseUrl}`, adres);
  }

  public updateAdres(adres: Adres) : Observable<Adres>{
    return this.http.put<Adres>(`${this.baseUrl}`, adres);
  }

  public deleteAdres(adresId: number) : Observable<any>{
    return this.http.delete(`${this.baseUrl}/${adresId}`);
  }

  constructor(private http: HttpClient) { }
}
