import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Status } from '../data/Status';

@Injectable({
  providedIn: 'root'
})
export class StatusService {
  private url: string = "https://localhost:7240/api/status"

  constructor(private http: HttpClient) { }

  getStatuses() : Observable<Status[]>
  {
    return this.http.get<Status[]>(this.url)
  }
}
