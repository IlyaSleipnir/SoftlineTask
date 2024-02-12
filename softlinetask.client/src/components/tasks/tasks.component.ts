import { StatusService } from './../../services/status.service';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../../data/Task';
import { TasksService } from '../../services/tasks.service';
import { Status } from '../../data/Status';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent {
  selectedTask?: Task;
  columnsHeaders : string[] = ["name", "description", "status"]

  // form : FormGroup

  Tasks$ : Observable<Task[]>
  Statuses$ : Observable<Status[]>

  newTask? : Task

  constructor(
    private tasksService : TasksService, 
    private statusService : StatusService,
    // private fb: FormBuilder
    ) {
    this.Tasks$ = this.tasksService.getAllTasks()
    this.Statuses$ = this.statusService.getStatuses()
    // this.form = this.fb.group(
    //   {
    //     name: ['', Validators.required],
    //     description: ['', Validators.required],
    //     statusId: [undefined, Validators.required]
    //   }
    // )
  }

  onSelect(task : Task) : void {
    console.log(task)
    if (!this.selectedTask)
      this.selectedTask = task
    else
      this.selectedTask = undefined
    this.newTask = undefined
  }

  cancle() : void {
    this.selectedTask = undefined
    this.newTask = undefined
  }

  saveEdit() : void {
    
    if (this.selectedTask !== undefined)
    {
      console.log(this.selectedTask)
      this.tasksService.updateTask(this.selectedTask).subscribe(
        res => {
          console.log('Data updated successfully:', res)
          this.Tasks$ = this.tasksService.getAllTasks()
        },
        err => {
          console.log('Failed to update data:', err)
        }
      )
      this.selectedTask = undefined
    }
  }

  deleteTask(): void {
    if (this.selectedTask !== undefined && this.selectedTask.id !== undefined)
    {
      this.tasksService.deleteTask(this.selectedTask.id).subscribe(
        res => {
          console.log('Delete success')
          this.Tasks$ = this.tasksService.getAllTasks()
        },
        err => {
          console.log('Delete failed')
        }
      )
      this.selectedTask = undefined
    }
  }

  addTask() : void {
    if(!this.newTask)
      this.newTask = {
        id: 0,
        name: '',
        description: '',
        statusId: 0,
        status: {name: '', id: 0} as Status
      }
    else
      this.newTask = undefined
    this.selectedTask = undefined
  }

  saveAddTask() : void {
    if (this.newTask !== undefined)
    {
      console.log(this.newTask)
      this.tasksService.postTask(this.newTask).subscribe(
        res => {
          console.log('Data updated successfully:', res)
          this.Tasks$ = this.tasksService.getAllTasks()
        },
        err => {
          console.log('Failed to update data:', err)
        })
        this.newTask = undefined
    }
  }
}
