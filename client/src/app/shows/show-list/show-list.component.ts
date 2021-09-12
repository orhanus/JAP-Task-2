import { Component, OnInit } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/_models/paginations';
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
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 3;


  constructor(private showsService: ShowsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadShows('movie');
  }

  loadShows(showType: string){
    this.showsService.getShows(showType, this.pageNumber, this.pageSize).subscribe(response => {
      this.shows = response.result;
      this.pagination = response.pagination;
    }, error => {
      console.log(error);
    })
  }

  rateShow(rating: any) {
    this.showsService.addRating(rating).subscribe(response => {
      this.toastr.success("Rating was a success", "Success");
    }, error => {
      console.log(error);
      this.toastr.error(error);
    })
  }
  onClick(showType: string)
  {
    this.loadShows(showType);
  }

}
