import { Component } from '@angular/core';
import { BoxInitialDescriptionComponent } from "../box-initial-description/box-initial-description.component";
import { BoxInitialCarouselComponent } from "../box-initial-carousel/box-initial-carousel.component";
import { BoxInitialCardsComponent } from "../box-initial-cards/box-initial-cards.component";

@Component({
    selector: 'app-box-initial',
    standalone: true,
    templateUrl: './box-initial.component.html',
    styleUrl: './box-initial.component.css',
    imports: [BoxInitialDescriptionComponent, BoxInitialCarouselComponent, BoxInitialCardsComponent]
})
export class BoxInitialComponent {

}
