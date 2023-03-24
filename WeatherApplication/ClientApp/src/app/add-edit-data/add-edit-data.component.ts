import { Component, Inject, Input, OnInit } from '@angular/core';
import { WeatherdataComponent } from '../weatherdata/weatherdata.component';

@Component({
  selector: 'app-add-edit-data',
  templateUrl: './add-edit-data.component.html',
  styleUrls: ['./add-edit-data.component.css']
})
export class AddEditDataComponent implements OnInit {

  constructor(private weatherdatacomponent: WeatherdataComponent) {
  }

  @Input() weatherSelection: any;
  weatherdataId!: number;
  temperature!: number;
  rainfall!: number;
  windSpeed!: number;
  localityId!: number;
  day!: string;

  ngOnInit(): void {
    this.weatherdataId = this.weatherSelection.weatherdataId;
    this.temperature = this.weatherSelection.temperature;
    this.rainfall = this.weatherSelection.rainfall;
    this.windSpeed = this.weatherSelection.windSpeed;
    this.localityId = this.weatherSelection.localityId;
    this.day = this.weatherSelection.day;
  }

  addNewData() {
    var val = {
      weatherdataId:this.weatherdataId,
      isEmpty: false,
      temperature: this.temperature,
      rainfall: this.rainfall,
      windSpeed: this.windSpeed,
      localityId: this.localityId,
      day: this.day,
    }
    if (val.temperature == null) { //Check if temperature field is set
      alert('Enter temperature')
    } else {
      this.weatherdatacomponent.addNewData(val);
    }
  }

  updateData() {
    var val = {
      weatherdataId: this.weatherdataId,
      isEmpty: false,
      temperature: this.temperature,
      rainfall: this.rainfall,
      windSpeed: this.windSpeed,
      localityId: this.localityId,
      day: this.day,
    }

    //Check if data was changed
    if (val.temperature == this.weatherSelection.temperature && val.rainfall == this.weatherSelection.rainfall && val.windSpeed == this.weatherSelection.windSpeed) {
      alert('No fields changed')
    } else if (val.temperature == null) {
      alert('Enter Temperature')
    } else {
      this.weatherdatacomponent.updateData(val);
    }
  }

  deleteData() {
    var idToDelete = this.weatherdataId
    console.log(idToDelete)
    this.weatherdatacomponent.deleteData(idToDelete)
  }
}
