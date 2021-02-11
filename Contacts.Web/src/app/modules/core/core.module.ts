import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from 'src/app/components/layout/layout.component';
import { MaterialModule } from '../material/material.module';
import { EntryItemComponent } from 'src/app/components/entry-item/entry-item.component';
import { EntryFormComponent } from 'src/app/components/entry-form/entry-form.component';
import { AppRoutingModule } from '../routing/routing.module';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [LayoutComponent, EntryItemComponent, EntryFormComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AppRoutingModule,
    FormsModule,
  ],
  exports: [LayoutComponent, EntryItemComponent, EntryFormComponent]
})
export class CoreModule { }
