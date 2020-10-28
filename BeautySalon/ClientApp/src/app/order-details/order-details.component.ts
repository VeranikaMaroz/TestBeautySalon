import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Customer } from '../models/customer';
import { Order, OrderDetail, Product } from '../models/order';
import { OrderService } from '../services/order.service';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  providers: [OrderService, CustomerService]
})
export class OrderDetailsComponent {
  order: Order;
  rootURL: string;
  editDetail: OrderDetail;
  public products: Product[];
  public customers: Customer[];

  orderId: string;
  product: Product;
  customer: Customer;
  date: Date = new Date();

  constructor(private orderService: OrderService, customerService: CustomerService, route: ActivatedRoute, private router: Router) {

    this.orderId = route.snapshot.params["orderId"];

    orderService.getProducts().subscribe(result => {
      this.products = result;
    }, error => console.error(error));

    customerService.getCustomers().subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    if (this.orderId == null || this.orderId != "00000000-0000-0000-0000-000000000000")
      this.refreshOrder();
    else
      this.order = new Order(null, null, null);
  }

  // сохранение данных
  save() {
    if (this.order == null)
      this.order = new Order(null, this.date, this.customer);
    else {
      this.order.date = this.date;
      this.order.customer = this.customer;
    }
    if (this.order.id == null) {
      this.order.id = "00000000-0000-0000-0000-000000000000";
      this.orderService.createOrder(this.order)
        .subscribe((data: Order) => {
          this.order = data;
          this.orderId = data.id;
        }, error => { console.error(error); this.cancel();}, () => { 
            this.router.navigate(['/order-details/' + this.orderId]);
          });
    } else {
      this.orderService.updateOrder(this.order)
        .subscribe(data => this.refreshOrder());
    }
  }

  saveDetail() {
    var url = this.rootURL + '/orderDetails'
    if (this.editDetail.id == null) {
      this.editDetail.id = "00000000-0000-0000-0000-000000000000";
      this.editDetail.orderId = this.order.id;
      this.orderService.createOrderDetail(this.editDetail)
        .subscribe((data: OrderDetail) => {
          this.order.orderDetails.push(data);
        }, error => { console.error(error); });
    } else {
      this.orderService.updateOrderDetail(this.editDetail)
        .subscribe(data => this.refreshOrder());
    }
    this.cancelDetail();
  }
  cancel() {
    this.router.navigate(['/all-orders']);
  }
  cancelDetail() {
    if (this.order.orderDetails && this.editDetail != null && (this.editDetail.id == "00000000-0000-0000-0000-000000000000" || this.editDetail.id == null)) {
      const index = this.order.orderDetails.findIndex(d => d.id === this.editDetail.id);
      if (index > -1) {
        this.order.orderDetails.splice(index, 1);
      }
    }
    this.editDetail = null;
  }

  editDetails(d: OrderDetail) {
    if (this.editDetail != null)
      this.cancelDetail();
    this.editDetail = d;
  }
  deleteDetail(orderDetail: OrderDetail) {
    this.orderService.deleteOrderDetail(orderDetail.id)
      .subscribe(data => {
        const index = this.order.orderDetails.indexOf(orderDetail);
        if (index > -1) {
          this.order.orderDetails.splice(index, 1);
        }
      });
  }
  add() {
    if (this.editDetail != null)
      this.cancelDetail();
    this.editDetail = new OrderDetail(null, 1, this.order.id);
    this.order.orderDetails.unshift(this.editDetail);
  }

  refreshOrder() {
    this.orderService.getOrder(this.orderId)
      .subscribe((data: Order) => { this.order = data; });
  }
}
