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
    // Depuração para verificar valores
    debugger;
  
    // Obtém a quantidade selecionada para o produto
    const quantity = this.quantities[product.id] || 0;
  
    // Valida a compra
    if (!this.validatePurchase(product, quantity)) return;
  
    // Cria o objeto de requisição para o pedido
    const orderRequest: any = {
      productId: product.id, // Corrige o nome do campo
      clientId: "12454", // Corrige o nome do campo para "clientId"
      quantity: quantity
    };
  
    // Chama o serviço para criar o pedido
    this.orderService.createOrder(orderRequest).subscribe({
      next: (response: any) => {
        // Atualiza o saldo e o estoque após a compra
        this.balance -= product.UnitPrice * quantity;
        product.stock -= quantity;
  
        // Exibe mensagem de sucesso
        alert('Compra realizada com sucesso!');
      },
      error: (err) => {
        // Trata erros retornados pela API
        const errorMessage = this.getErrorMessage(err);
        alert(`Erro na compra: ${errorMessage}`);
      }
    });
  }
  
  /**
   * Obtém a mensagem de erro de forma amigável.
   * @param err - Erro retornado pela API.
   * @returns Mensagem de erro amigável.
   */
  private getErrorMessage(err: any): string {
    if (err.error?.errors) {
      // Concatena todas as mensagens de erro de validação
      return Object.values(err.error.errors)
        .flat()
        .join(', ');
    }
    return err.error?.message || 'Ocorreu um erro. Tente novamente.';
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