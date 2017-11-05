import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
//Inclusione Componenti Aggiunti
import { UsersComponent } from './components/users/users.component';
import { ConcertsComponent } from './components/concerts/concerts.component';
import { MyConcertsComponent } from './components/myconcerts/myconcerts.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        UsersComponent,
        ConcertsComponent,
        MyConcertsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'users', component: UsersComponent },
            { path: 'concerts', component: ConcertsComponent },
            { path: 'myconcerts', component: MyConcertsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
