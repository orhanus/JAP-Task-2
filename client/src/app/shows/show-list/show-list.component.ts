import { Component, OnInit } from '@angular/core';
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
    this.showsService.getShows('all').subscribe(response => {
      this.shows = response;
    }, error => {
      console.log(error);
    })
  }

}
