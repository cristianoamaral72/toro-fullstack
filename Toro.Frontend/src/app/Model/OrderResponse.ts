export interface OrderResponse {
    isSuccess: boolean;
    response?: {
      productId: number;
      quantity: number;
      totalAmount: number;
      accountBalance: number;
    };
    errors?: string[];
  }
  