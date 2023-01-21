# Tugas Transaction dengan Service

```## Alur Kerja Transaction

### Cara Pertama
- Pendaftaran Customer
- Melakukan Pembelian dengan menginputkan barang dibeli `ProductPrice` berupa array

#### Model Data Transaction
```json
{
  "customerId": "guid",
  "purchaseDetails": [
    {
      "productPriceId": "guid",
      "qty": "int"
    },
    {
      "productPriceId": "guid",
      "qty": "int"
    },
    {
      "productPriceId": "guid",
      "qty": "int"
    }
  ]
}
```

### Cara Kedua
- Melakukan pembelian dengan memasukkan data customer dan memilih barang yang dibeli `ProductPrice` berupa array

#### Model Transaction
```json
{
  "customer": {
    "customerName": "string",
    "address": "string",
    "phoneNumber": "string",
    "email": "string"
  },
  "purchaseDetails": [
    {
      "productPriceId": "guid",
      "qty": "int"
    },
    {
      "productPriceId": "guid",
      "qty": "int"
    },
    {
      "productPriceId": "guid",
      "qty": "int"
    }
  ]
}
```

### Custom Response
```json
{
  "statusCode": "int",
  "message": ""
}
```