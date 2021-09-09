import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Show } from 'src/app/_models/show';
import { ShowsService } from 'src/app/_services/shows.service';

@Component({
  selector: 'app-show-card',
  templateUrl: './show-card.component.html',
  styleUrls: ['./show-card.component.css']
})
export class ShowCardComponent implements OnInit {
  @Input() show: Show;
  @Output() showRated = new EventEmitter()

  max = 10;
  rate = 7;
  isReadonly = false;
  overStar: number | undefined;
  constructor(private showService: ShowsService) { }

  ngOnInit(): void {
    this.rate = this.show.averageRating;
  }
 
 
  hoveringOver(value: number): void {
    this.overStar = value;
  }
 
  resetStar(): void {
    this.overStar = void 0;
  }
  onClick() {
    this.showRated.emit({showId: this.show.id, score: this.overStar});
    console.log("emited");
  }

}
