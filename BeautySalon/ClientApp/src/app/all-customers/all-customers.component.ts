import { Component } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-all-customers',
  templateUrl: './all-customers.component.html',
  providers: [CustomerService]
})
export class CustomersComponent {
  public customers: Customer[];
  editCustomer: Customer;

  constructor(private customerService: CustomerService) {
    this.refreshList();
  }

  refreshList() {
    this.customerService.getCustomers().subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }

  // сохранение данных
  save() {
    if (this.editCustomer.id == null) {
      this.editCustomer.id = "00000000-0000-0000-0000-000000000000";
      this.customerService.createCustomer(this.editCustomer)
        .subscribe((data: Customer) => {
          this.customers.unshift(data);
        }, error => { console.error(error); });
    } else {
      this.customerService.updateCustomer(this.editCustomer)
        .subscribe(data => this.refreshList());
    }
    this.cancel();
  }

  delete(p: Customer) {
    this.customerService.deleteCustomer(p.id)
      .subscribe(data => this.refreshList());
  }

  add() {
    if (this.editCustomer != null)
      this.cancel();
    this.editCustomer = new Customer(null, null, null);
    this.customers.unshift(this.editCustomer);
  }

  edit(p: Customer) {
    if (this.editCustomer != null)
      this.cancel();
    this.editCustomer = p;
  }

  cancel() {
    if (this.editCustomer != null) {
      const index = this.customers.findIndex(d => d.id === this.editCustomer.id);
      if (index > -1) {
        this.customers.splice(index, 1);
      }
    }
    this.editCustomer = null;
  }

}
