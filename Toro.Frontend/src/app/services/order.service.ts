import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, delay, Observable, of, throwError } from 'rxjs';
import { Account } from '../Model/Account';
import { OrderRequest } from '../Model/OrderRequest';
import { OrderResponse } from '../Model/OrderResponse';
import { environment } from '../../environments/environment';
import { MOCK_PRODUCTS } from 'app/mocks/mock-products';
import { Product } from 'app/Model/Product';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private readonly apiUrl = environment.apiUrl;
  private readonly mockProducts = MOCK_PRODUCTS;

  constructor(private http: HttpClient) {}

  /**
   * Retorna a lista de produtos disponíveis.
   * @returns Observable<Product[]>
   */
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/products`).pipe(
      catchError((error) => {
        console.error('Erro ao buscar produtos:', error);
        return throwError(() => new Error('Erro ao buscar produtos.'));
      })
    );
  }

  /**
   * Retorna a lista de produtos mockados (simulação).
   * @returns Observable<Product[]>
   */
  getProductsMock(): Observable<Product[]> {
    // Simula um delay de API
    return of(this.mockProducts).pipe(delay(500));
  }

  /**
   * Obtém o saldo da conta do cliente.
   * @param clientId - ID do cliente.
   * @returns Observable<Account>
   */
  getAccount(clientId: string): Observable<Account> {
    return this.http.get<Account>(`${this.apiUrl}/account/${clientId}`).pipe(
      catchError((error) => {
        console.error(`Erro ao obter a conta do cliente ${clientId}:`, error);
        return throwError(() => new Error('Erro ao obter informações da conta.'));
      })
    );
  }

  /**
   * Realiza a compra de um produto.
   * @param order - Pedido do cliente.
   * @returns Observable<OrderResponse>
   */
  placeOrder(order: OrderRequest): Observable<OrderResponse> {
    return this.http.post<OrderResponse>(`${this.apiUrl}/Orders`, order).pipe(
      catchError((error) => {
        console.error('Erro ao realizar o pedido:', error);
        return throwError(() => new Error('Erro ao realizar o pedido.'));
      })
    );
  }

  /**
   * Cria um novo pedido.
   * @param orderRequest - Dados do pedido.
   * @returns Observable<OrderResponse>
   */
  createOrder(orderRequest: OrderRequest): Observable<OrderResponse> {
    return this.http.post<OrderResponse>(`${this.apiUrl}/Orders`, orderRequest).pipe(
      catchError((error) => {
        console.error('Erro ao criar o pedido:', error);
        return throwError(() => new Error('Erro ao criar o pedido.'));
      })
    );
  }
}