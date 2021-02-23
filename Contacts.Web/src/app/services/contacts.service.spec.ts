import { ContactsService } from './contacts.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('ContactsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientTestingModule, HttpClient, HttpHeaders ],
    providers: [ ContactsService ]
  }));

 it('should be created', () => {
    const service: ContactsService = TestBed.inject(ContactsService);
    expect(service).toBeTruthy();
   });
});