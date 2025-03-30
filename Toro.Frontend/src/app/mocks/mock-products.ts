import { Product } from "app/Model/Product";

export const MOCK_PRODUCTS: Product[] = [
  {
    id: '1',
    bondAsset: 'CDB Prefixado',
    index: 'IPCA',
    tax: 12.5,
    issuerName: 'Banco Toro',
    unitPrice: 1000,
    stock: 150,
    quantity: 0 // Add the missing property
  },
  {
    id: '2',
    bondAsset: 'LCI',
    index: 'Pré',
    tax: 9.2,
    issuerName: 'Banco ABC',
    unitPrice: 2500,
    stock: 80,
    quantity: 0 // Add the missing property
  },
  {
    id: '3',
    bondAsset: 'Debênture',
    index: 'IGPM',
    tax: 11.8,
    issuerName: 'Energia Brasil',
    unitPrice: 3500,
    stock: 45,
    quantity: 0 // Add the missing property
  }
];