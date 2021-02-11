  
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Entry } from '../classes/entry';
import { EntrySearch } from '../classes/entrySearch';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  constructor(private http: HttpClient) { }

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
  getEntries() {
    let phonebookId = "00000000-0000-0000-0000-000000000000";
    return this.http.get<Entry[]>("https://localhost:5001/phonebookentries?" + phonebookId, {headers: this.headers});
  }

  addEntry(entry: Entry) {

    entry.id = "00000000-0000-0000-0000-000000000000";
    entry.phonebookId = "00000000-0000-0000-0000-000000000000";
    return this.http.post<string>("https://localhost:5001/entry/insert", entry, {headers: this.headers});
  }

  getEntry(entryId: string) {
    return this.http.get<Entry>("https://localhost:5001/entry/get?entryId=" + entryId, {headers: this.headers});
  }

  searchEntries(searchText: string ){
    let entrySearch:  EntrySearch  = {
      searchText: searchText,
      phoneBookId: "00000000-0000-0000-0000-000000000000"
    };
    return this.http.post<Array<Entry>>("https://localhost:5001/entry/Search", entrySearch, {headers: this.headers});
  }
}
