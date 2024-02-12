import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksComponent } from '../components/tasks/tasks.component';
// import { InputComponent } from '../components/input/input.component';

const routes: Routes = [
  {path: "", component: TasksComponent},
  // {path: "", component: InputComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
