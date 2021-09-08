import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Show } from './_models/show';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Rotten Potatoes';
  shows: Show[];

  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.getShows();
  }

  getShows() {
    this.http.get<Show[]>('https://localhost:5001/api/shows/all').subscribe(response => {
      console.log(response);
      this.shows = response;
    }, error => {
      console.log(error);
    });
  }
}
