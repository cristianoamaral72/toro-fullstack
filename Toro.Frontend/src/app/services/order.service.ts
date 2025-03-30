import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, Observable, of } from 'rxjs';
import { Account } from '../Model/Account';
import { OrderRequest } from '../Model/OrderRequest';
import { OrderResponse } from '../Model/OrderResponse';
import { environment } from '../../environments/environment';
import { MOCK_PRODUCTS } from 'app/mocks/mock-products';
import { Product } from 'app/Model/Product';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private mockProducts = MOCK_PRODUCTS;

  constructor(private http: HttpClient) {}

  private apiUrl = environment.apiUrl;

  // Buscar a lista de produtos
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/products`);
  }

  getProductsMock(): Observable<Product[]> {
    // Simula delay de API
    return of(this.mockProducts).pipe(delay(500));
  }

  // Obter o saldo do usuário
  getAccount(clientId: string): Observable<any> {
    return this.http.get<Account>(`${this.apiUrl}/account/${clientId}`);
  }

  // Realizar a compra de um produto
  placeOrder(order: OrderRequest): Observable<OrderResponse> {
    return this.http.post<OrderResponse>(`${this.apiUrl}/order`, order);
  }

  createOrder(orderRequest: OrderRequest) {
    return this.http.post<OrderResponse>(`${this.apiUrl}/order`, orderRequest);
  }
}
