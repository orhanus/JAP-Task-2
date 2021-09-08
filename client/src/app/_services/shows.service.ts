import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Show } from '../_models/show';

@Injectable({
  providedIn: 'root'
})
export class ShowsService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getShows(showType: string){
    return this.http.get<Show[]>(this.baseUrl + 'shows/' + showType);
  }
}
