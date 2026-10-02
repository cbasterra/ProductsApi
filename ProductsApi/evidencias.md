# Evidencias

## Etapa 8 — POST inválido (nombre vacío, precio 100)

Request: `POST /api/Products` con body `{"name": "", "price": 100}`

Respuesta: `400 Bad Request`

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": [
      "El nombre es obligatorio.",
      "El nombre debe tener entre 3 y 100 caracteres."
    ]
  },
  "traceId": "00-df0c2947eb710b0c668572c8dbe61722-618b92eb669e374a-00"
}
```