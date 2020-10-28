import { Customer } from '../models/customer';

export class Order {
  constructor(
    public id: string,
    public date: Date,
    public customer: Customer,
    public totalPrice: number = null,
    public orderDetails: OrderDetail[] = []) { }
}

export class OrderDetail {
  constructor(
    public id: string,
    public quantity: number,
    public orderId: string,
    public product: Product = null) { }
}

export interface Product {
  id: string;
  name: string;
  price: number;
}
