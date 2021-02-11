import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Entry } from 'src/app/classes/entry';
import { ContactsService } from 'src/app/services/contacts.service';

@Component({
  selector: 'app-entry-form',
  templateUrl: './entry-form.component.html',
  styleUrls: ['./entry-form.component.css']
})
export class EntryFormComponent implements OnInit {

  entry: Entry = {
    name: '',
    id: '',
    phoneNumber: '',
    phonebookId: ''
  };

  constructor(private contactsService: ContactsService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    
    this.router.events.subscribe(
      () =>{
        const entryId = routeParams.get('id');
        if(entryId) {
          this.getEntry(entryId);
        }
      }
    );
  }
  public selectedId: string = "";
  getEntry(entryId: string) {
    //if (this.selectedId != entryId)
    {
      this.selectedId = entryId;
      this.contactsService.getEntry(entryId).subscribe(
        data => { this.entry = data; },
        err => {},
        () => {}
      )
    }
  }

  save() {
    this.contactsService.addEntry(this.entry).subscribe(
      data => {this.router.navigate(['/'])},
      err => {},
      () => {}
    )
  }

  cancel() {
    this.entry = {
      name: '',
      id: '',
      phoneNumber: '',
      phonebookId: ''
    }
  }

}
