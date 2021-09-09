import { Component, OnInit } from '@angular/core';
import { Rating } from 'src/app/_models/rating';
import { Show } from 'src/app/_models/show';
import { ShowsService } from 'src/app/_services/shows.service';

@Component({
  selector: 'app-show-list',
  templateUrl: './show-list.component.html',
  styleUrls: ['./show-list.component.css']
})
export class ShowListComponent implements OnInit {
  shows: Show[];

  constructor(private showsService: ShowsService) { }

  ngOnInit(): void {
    this.loadShows('movie');
  }

  loadShows(showType: string){
    this.showsService.getShows(showType).subscribe(response => {
      this.shows = response;
    }, error => {
      console.log(error);
    })
  }

  rateShow(rating: any) {
    this.showsService.addRating(rating).subscribe(response => {
      this.shows = response;
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  onClick(showType: string)
  {
    this.loadShows(showType);
  }

}
