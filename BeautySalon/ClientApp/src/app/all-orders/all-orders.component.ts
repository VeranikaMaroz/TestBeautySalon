import { Component, Inject } from '@angular/core';
import { Order } from '../models/order';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  providers: [OrderService]
})

export class OrdersComponent {
  public orders: Order[];

  constructor(private orderService: OrderService) {
    this.refreshList();
  }

  delete(order: Order) {
    if (confirm('Are you sure to delete this record?')) {
      this.orderService.deleteOrder(order.id)
        .subscribe(data => this.refreshList());
    }
  }

  refreshList() {
    this.orderService.getOrders().subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }

}
