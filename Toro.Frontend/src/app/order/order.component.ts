import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { AccountService } from '../services/account.service';
import { Product } from 'app/Model/Product';
import { OrderRequest } from 'app/Model/OrderRequest';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  products: Product[] = [];
  balance: number = 0;
  quantities: { [key: string]: number } = {};

  constructor(
    private orderService: OrderService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadBalance();
  }

  loadProducts(): void {
    this.orderService.getProductsMock().subscribe({
      next: (products) => {
        this.products = products.sort((a: any, b: any) => b.Tax - a.Tax);
      },
      error: (err) => console.error('Error loading products:', err)
    });
  }

  loadBalance(): void {
    this.accountService.getAccount().subscribe({
      next: (account: any) => {
        this.balance = account.Balance;
      },
      error: (err) => console.error('Error loading balance:', err)
    });
  }

  purchase(product: any): void {
    const quantity = this.quantities[product.id] || 0;
    
    if (!this.validatePurchase(product, quantity)) return;

    const orderRequest: any = {
      productId: product.id,
      quantity: quantity
    };

    this.orderService.createOrder(orderRequest).subscribe({
      next: (response: any) => {
        this.balance -= product.UnitPrice * quantity;
        product.Stock -= quantity;
        alert('Compra realizada com sucesso!');
      },
      error: (err) => alert(`Erro na compra: ${err.error?.message || 'Tente novamente'}`)
    });
  }

  private validatePurchase(product: any, quantity: number): boolean {
    
    if (quantity <= 0) {
      alert('Quantidade inválida');
      return false;
    }
    
    if (quantity > product.Stock) {
      alert('Quantidade excede o estoque disponível');
      return false;
    }

    const totalCost = product.UnitPrice * quantity;
    if (totalCost > this.balance) {
      alert('Saldo insuficiente');
      return false;
    }

    return true;
  }
}