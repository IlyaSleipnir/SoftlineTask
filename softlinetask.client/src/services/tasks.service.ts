import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../data/Task';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private url: string = "https://localhost:7240/api/worktasks"

  constructor(private http: HttpClient) { }

  getAllTasks() : Observable<Task[]> {
    return this.http.get<Task[]>(this.url)
  }

  getTask(id : number ) : Observable<Task> {
    return this.http.get<Task>(`${this.url}/${id}`)
  }

  postTask(task : Task) : Observable<Task> {
    return this.http.post<Task>(this.url, task, httpOptions)
  }

  updateTask(task : Task) : Observable<Task> {
    return this.http.put<Task>(`${this.url}/${task.id}`, task, httpOptions)
  }

  deleteTask(id : number) : Observable<Task> {
    return this.http.delete<Task>(`${this.url}/${id}`)
  }
}
