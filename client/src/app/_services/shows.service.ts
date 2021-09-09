import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rating } from '../_models/rating';
import { Show } from '../_models/show';

@Injectable({
  providedIn: 'root'
})
export class ShowsService {
  baseUrl = 'https://localhost:5001/api/shows/';

  constructor(private http: HttpClient) { }

  getShows(showType: string){
    return this.http.get<Show[]>(this.baseUrl + showType);
  }
  addRating(rating: Rating){
    return this.http.post<Show[]>(this.baseUrl + 'add-rating', rating);
  }
}
