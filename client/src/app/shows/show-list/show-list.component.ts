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
    this.loadShows();
  }

  loadShows(){
    this.showsService.getShows('all').subscribe(response => {
      this.shows = response;
    }, error => {
      console.log(error);
    })
  }

  rateShow(rating: any) {
    console.log("parent");
    console.log(rating);
    this.showsService.addRating(rating).subscribe(response => {
      this.shows = response;
    }, error => {
      console.log(error);
    })
  }

}
