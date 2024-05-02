import { Component } from '@angular/core';
import { NavbarComponent } from "../../components/navbar/navbar.component";
import { DisplayTitleComponent } from "../../components/display-title/display-title.component";
import { BoxInitialComponent } from "../../components/box-initial/box-initial.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [NavbarComponent, DisplayTitleComponent, BoxInitialComponent]
})
export class HomeComponent {

}
