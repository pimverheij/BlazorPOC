import { Deserializable } from './deserializable.model';

export class Adres implements Deserializable {

  constructor(public land:string='Nederland'){
  }

  deserialize(input: any): this {
    return Object.assign(this, input);
  }
    public id: number;
    public straat: string;
    public adresRegel1: string;
    public adresRegel2: string;
    public postCode: string;
    public huisnummer: number;
    public huisnummerToevoeging: string;
    public gemeente: string;

  public isBinnenlandsAdres() {
    return this.land?.toLowerCase() === 'nederland' || !this.land;
  }
}
