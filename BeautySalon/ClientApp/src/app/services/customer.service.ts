import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { Observable } from 'rxjs';

@Injectable()
export class CustomerService {

  private url = "/api/customer";

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    this.url = baseUrl + this.url;
  }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.url);
  }

  createCustomer(customer: Customer) {
    return this.http.post(this.url, customer);
  }

  updateCustomer(product: Customer) {
    return this.http.put(this.url, product);
  }

  deleteCustomer(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
