import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { AddEditDataComponent } from '../add-edit-data/add-edit-data.component';
import { LocalityComponent } from '../locality/locality.component';


@Component({
  selector: 'app-weatherdata',
  templateUrl: './weatherdata.component.html',
  styleUrls: ['./weatherdata.component.css']
})

export class WeatherdataComponent implements OnInit {
  http: HttpClient;
  baseUrl: string;
  modalReference!: NgbModalRef;

  year!: number;
  month!: string;
  daysInThisMonth: number = 0;

  locations: any[] = [];
  currentLocation!: any;
  weatherSelection!: WeatherData;
  public weatherData: WeatherData[] = [];
  public days: number[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private localityComponent: LocalityComponent) {
    this.http = http;
    this.baseUrl = baseUrl;
    
  }
  ngOnInit(): void {
    var now = new Date();
    this.month = localStorage.getItem('currentMonth') || (now.getMonth() + 1).toString()
    this.year = Number(localStorage.getItem('currentYear')) || now.getFullYear()
    console.log(this.month)
    console.log(this.year)
    this.localityComponent.getLocations().subscribe(locations => {
      this.locations = locations
      this.currentLocation = JSON.parse(localStorage.getItem('currentLocation')!) || this.locations[0]
      console.log(this.currentLocation)
      this.getData();
    })
  } 

  async getData() {
    await this.http.get<WeatherData[]>(this.baseUrl + 'weatherData/' + this.currentLocation.localityName + '/' + this.year + '/' + this.month).subscribe(result => {
      this.weatherData = result;
      this.populateWeatherArray();
    }, error => console.error(error));
  }

  //Open modal window for editing or adding data
  openModal(content: any, item: WeatherData) {
    this.modalReference = this.modalService.open(content, { backdrop: 'static', keyboard : false });
    this.modalReference.result.then(res => {});
    this.weatherSelection = item;
  }

  async addNewData(addItem: WeatherData) {
    console.log('addNewData()');
    console.log(addItem);

    await this.http.post<WeatherData>(this.baseUrl + 'weatherData', addItem).subscribe(result => {
      console.log('New item added');
      this.getData() //Refresh data
      this.modalReference.close(); //Close modal window
    }, error => alert(error.message))
  }

  async updateData(updateItem: WeatherData) {
    console.log('updateData()');
    console.log(updateItem);

    await this.http.put<WeatherData>(this.baseUrl + 'weatherData', updateItem).subscribe(result => {
      console.log('Item updated');
      this.getData() //Refresh data
      this.modalReference.close(); //Close modal window
    }, error => alert(error.message))
  }

  async deleteData(id: number) {
    if (confirm("Are you sure to delete this data?")) {
      await this.http.delete(this.baseUrl + 'weatherData/' + id).subscribe(result => {
        this.modalReference.close(); //Close modal window
        alert("item deleted!")
        this.getData() //Refresh data
      })
    } else {
      console.log("Deletion not completed!")
    }
  }

  changeMonth(value: string) {
    if (this.month != value) {
      console.log('Month now: ' + value)
      this.month = value;
      localStorage.setItem('currentMonth', this.month);
      this.getData() //Get new data
    }
  }
  changeYear(value: number) {
    if (this.year != value) {
      console.log('Year now: ' + value)
      this.year = value;
      localStorage.setItem('currentYear', this.year.toString());
      this.getData() //Get new data
    }
  }

  changeLocation(value: any) {
    if (this.currentLocation != value) {
      this.currentLocation = value
      localStorage.setItem('currentLocation', JSON.stringify(value))
      console.log('Curren Location now: ' + this.currentLocation.localityName)
      this.getData() //Get new data
    }
  }

  //Populate weather data array if no data is found from database.
  populateWeatherArray() {
    this.daysInThisMonth = getMonthDaysCount(this.year + "-" + this.month)
    this.days = Array(this.daysInThisMonth).fill(1).map((x, i) => i + 1);
    for (let i of this.days) {

      var date = this.year + '-' + this.month.padStart(2, '0') + '-' + i.toString().padStart(2, '0');

      if (!this.weatherData.some(e => e.day.substring(0, 10) === date)) {
        let newItem = {} as WeatherData;
        newItem.day = date
        newItem.localityId = this.currentLocation.localityId
        newItem.isEmpty = true; //Set isEmpty to true so newitems can be identified later. 
        this.weatherData.push(newItem)
        //console.log(date)
        //console.log(newItem)
      }
    }
    //Sort weather data array
    this.weatherData.sort((a, b) => (a.day < b.day ? -1 : 1));
  }
}


const getMonthDaysCount = (date: string | Date): number => {
    const tmp = new Date(date);
    tmp.setMonth(tmp.getMonth() + 1);
    tmp.setDate(0);
    return tmp.getDate();
  };

interface WeatherData {
  weatherdataId: number;
  isEmpty: boolean;
  temperature: number;
  rainfall: number;
  windSpeed: number;
  localityId: number;
  day: string;
}
