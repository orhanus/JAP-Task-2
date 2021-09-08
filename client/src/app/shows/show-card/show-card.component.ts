import { Component, Input, OnInit } from '@angular/core';
import { Show } from 'src/app/_models/show';

@Component({
  selector: 'app-show-card',
  templateUrl: './show-card.component.html',
  styleUrls: ['./show-card.component.css']
})
export class ShowCardComponent implements OnInit {
  @Input() show: Show;
  max = 10;
  rate = 7;
  isReadonly = false;
  overStar: number | undefined;
  percent = 0;
  constructor() { }

  ngOnInit(): void {
    this.rate = this.show.averageRating;
  }
 
 
  hoveringOver(value: number): void {
    this.overStar = value;
    this.percent = (value / this.max) * 100;
  }
 
  resetStar(): void {
    this.overStar = void 0;
  }

}
