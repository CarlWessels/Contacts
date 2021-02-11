import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { EntryFormComponent } from 'src/app/components/entry-form/entry-form.component';

const routes: Routes = [
  { path: 'entry', component: EntryFormComponent },
  { path: 'entry/:id', component: EntryFormComponent },
]; // sets up routes constant where you define your routes

// configures NgModule imports and exports
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
