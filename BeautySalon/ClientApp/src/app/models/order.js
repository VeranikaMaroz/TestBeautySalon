"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.OrderDetail = exports.Order = void 0;
var Order = /** @class */ (function () {
    function Order(id, date, customer, totalPrice, orderDetails) {
        if (totalPrice === void 0) { totalPrice = null; }
        if (orderDetails === void 0) { orderDetails = []; }
        this.id = id;
        this.date = date;
        this.customer = customer;
        this.totalPrice = totalPrice;
        this.orderDetails = orderDetails;
    }
    return Order;
}());
exports.Order = Order;
var OrderDetail = /** @class */ (function () {
    function OrderDetail(id, quantity, orderId, product) {
        if (product === void 0) { product = null; }
        this.id = id;
        this.quantity = quantity;
        this.orderId = orderId;
        this.product = product;
    }
    return OrderDetail;
}());
exports.OrderDetail = OrderDetail;
//# sourceMappingURL=order.js.map