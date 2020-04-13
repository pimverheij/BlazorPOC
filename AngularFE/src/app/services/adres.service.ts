import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Adres } from '../adres/adres.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdresService {

  private baseUrl: string = 'http://localhost:65157/adres';

  public getAdres(id: number) : Observable<Adres> {
    return this.http.get<Adres>(`${this.baseUrl}/${id}`,);
  }

  public getAdressen() : Observable<Adres[]> {
    return this.http.get<Adres[]>(`${this.baseUrl}`);
  }

  public addAdres(adres: Adres) : Observable<Adres>{
    return this.http.post<Adres>(`${this.baseUrl}`, adres);
  }

  public updateAdres(adres: Adres) : Observable<Adres>{
    return this.http.put<Adres>(`${this.baseUrl}`, adres);
  }

  constructor(private http: HttpClient) { }
}
