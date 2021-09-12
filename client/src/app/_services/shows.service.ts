import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/paginations';
import { Rating } from '../_models/rating';
import { Show } from '../_models/show';

@Injectable({
  providedIn: 'root'
})
export class ShowsService {
  baseUrl = 'https://localhost:5001/api/shows/';
  paginatedResult: PaginatedResult<Show[]> = new PaginatedResult<Show[]>();

  constructor(private http: HttpClient) { }

  getShows(showType: string, page?: number, itemsPerPage?: number){
    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<Show[]>(this.baseUrl + showType, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null)
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));

        return this.paginatedResult;
      })
    );
  }
  addRating(rating: Rating){
    return this.http.post<Show[]>(this.baseUrl + 'add-rating', rating);
  }
}
