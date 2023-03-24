import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LocalityComponent } from './locality/locality.component';
import { WeatherdataComponent } from './weatherdata/weatherdata.component';
import { AddEditDataComponent } from './add-edit-data/add-edit-data.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LocalityComponent,
    WeatherdataComponent,
    AddEditDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: WeatherdataComponent, pathMatch: 'full' },
      { path: 'localities', component: LocalityComponent },
      { path: 'weatherdata', component: WeatherdataComponent },
    ]),
    NgbModule
  ],
  providers: [LocalityComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
