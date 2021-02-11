import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Entry } from 'src/app/classes/entry';
import { ContactsService } from 'src/app/services/contacts.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  entries: Entry[] = [];
  constructor(private contactsService: ContactsService, private router: Router) {
    
   }

  ngOnInit(): void {
    this.router.events.subscribe(
          () => this.getEntries()
      );
  }

  getEntries() {
    this.contactsService.getEntries().subscribe(
      data => { this.entries = data; },
      err => {},
      () => {}
    );
  }

  addEntry() {
    this.router.navigate(['/entry'])
  }

  onKeypressEvent(event: any){
    
  }
  public searchText: string = "";
  onKeydown(event: any) {
    if (event.key === "Backspace") {
      this.searchText = this.searchText.substr(0, this.searchText.length - 1);
    }
    else if (event.key.length === 1) {
      this.searchText += event.key;
    }

    this.contactsService.searchEntries(this.searchText).subscribe(
      data => {this.entries = data;},
      err => {},
      () => {}
    )
  }
}
