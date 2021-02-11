import { Component, Input, OnInit } from '@angular/core';
import { Entry } from 'src/app/classes/entry';

@Component({
  selector: 'app-entry-item',
  templateUrl: './entry-item.component.html',
  styleUrls: ['./entry-item.component.css']
})
export class EntryItemComponent implements OnInit {
  @Input() entry: Entry = {
    name: '',
    id: '',
    phoneNumber: '',
    phonebookId: ''
  };
  
  constructor() { }

  ngOnInit(): void {
  }

}
