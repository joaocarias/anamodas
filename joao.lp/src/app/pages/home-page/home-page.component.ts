import { Component } from '@angular/core';
import { NavbarComponent } from "../../components/shared/navbar/navbar.component";
import { HeaderDefaultComponent } from "../../components/shared/header-default/header-default.component";
import { AboutDefaultComponent } from "../../components/shared/about-default/about-default.component";
import { ListServicesComponent } from "../../components/shared/list-services/list-services.component";
import { PortfolioComponent } from "../../components/shared/portfolio/portfolio.component";
import { LinkContactComponent } from "../../components/shared/link-contact/link-contact.component";
import { ContactComponent } from "../../components/shared/contact/contact.component";
import { FooterDefaultComponent } from "../../components/shared/footer-default/footer-default.component";

@Component({
    selector: 'app-home-page',
    standalone: true,
    templateUrl: './home-page.component.html',
    imports: [NavbarComponent, HeaderDefaultComponent, AboutDefaultComponent, ListServicesComponent, PortfolioComponent, LinkContactComponent, ContactComponent, FooterDefaultComponent]
})
export class HomePageComponent {

}
