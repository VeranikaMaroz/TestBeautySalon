import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { Order, OrderDetail, Product } from '../models/order';

@Injectable()
export class OrderService {

  private url = "/api/order";
  private detailsUrl = "/api/order/orderDetails";
  private productUrl = "/api/product";

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    this.url = baseUrl + this.url;
    this.detailsUrl = baseUrl + this.detailsUrl;
    this.productUrl = baseUrl + this.productUrl;
  }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.url);
  }

  getOrder(id: string) {
    return this.http.get(this.url + '/' + id);
  }

  createOrder(order: Order) {
    return this.http.post(this.url, order);
  }

  updateOrder(order: Order) {
    return this.http.put(this.url, order);
  }

  deleteOrder(id: string) {
    return this.http.delete(this.url + '/' + id);
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productUrl);
  }

  createOrderDetail(orderDet: OrderDetail) {
    return this.http.post(this.detailsUrl, orderDet);
  }

  updateOrderDetail(orderDet: OrderDetail) {
    return this.http.put(this.detailsUrl, orderDet);
  }

  deleteOrderDetail(id: string) {
    return this.http.delete(this.detailsUrl + '/' + id);
  }
}
