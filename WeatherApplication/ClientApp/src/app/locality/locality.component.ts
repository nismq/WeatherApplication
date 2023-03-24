import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-locality',
  templateUrl: './locality.component.html',
  styleUrls: ['./locality.component.css']
})
export class LocalityComponent implements OnInit{
  http: HttpClient;
  baseUrl: string;

  newLocation!: string;
  public localities: Locality[] = [];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.getLocations().subscribe(data => {
      this.localities = data
      console.log(this.localities)
    });
  }

  ngOnInit(): void {
    
  }

  getLocations(): Observable<Locality[]> {
    return this.http.get<Locality[]>(this.baseUrl + 'locality');
  }

  async addNewLocation() {
    var val = {
      localityName: this.newLocation
    }
    await this.http.post<Locality[]>(this.baseUrl + 'locality', val).subscribe(result => {
      this.localities = result;
    })
  }

  async deleteLocation(item: Locality) {
    await this.http.delete<Locality[]>(this.baseUrl + 'locality/' + item.localityId).subscribe(result => {
      this.localities = result;
    })
    console.log(item)
  }

}

interface Locality {
  localityId: number;
  localityName: string;
}
