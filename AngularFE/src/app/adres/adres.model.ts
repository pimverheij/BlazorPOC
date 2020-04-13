export class Adres {
  constructor(
    public id: number,
    public straat: string,
    public adresRegel1: string,
    public adresRegel2: string,
    public postCode: string,
    public huisnummer: number,
    public huisnummerToevoeging: string,
    public gemeente: string,
    public land: string,
  ) { }
}
