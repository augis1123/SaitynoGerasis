# API

### Sellers

#### `GET` api/sellers

Returns list of sellers.

##### Parameters

#### `GET` api/sellers/{sellersId}

Returns a single seller, specified by the sellerId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |

#### `POST` api/sellers

Creates a new seller.

#### `PUT` api/sellers/{sellersId}

Updates a single seller, specified by the sellersId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |

#### `DELETE` api/sellers/{sellersId}

Deletes a single seller, specified by the sellerId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |

### Posts

#### `GET` api/sellers/{sellersId}/items

Returns list of items from specified seller.

##### Parameters

| Name       | Required | Default value |
|------------|----------|---------------|
| sellersId  | required |               |
#### `GET` api/sellers/{sellersId}/items/{itemsId}

Returns a single item, specified by the itemsId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |
| itemsId   | required |               |

#### `POST` api/sellers/{sellersId}/items

Creates a new item.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |

#### `PUT` api/sellers/{sellersId}/items/{itemsId}

Updates a single item, specified by the itemsId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |
| itemsId   | required |               |

#### `DELETE` api/sellers/{sellersId}/items/{itemsId}

Deletes a single item, specified by the itemsId parameter.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |
| itemsId   | required |               |

### Comments

#### `GET` api/sellers/{sellersId}/items/{itemsId}/bills

Returns list of bills.

##### Parameters

| Name       | Required | Default value |
|------------|----------|---------------|
| sellersId  | required |               |
| itemsId    | required |               |

#### `GET` api/sellers/{sellersId}/items/{itemsId}/bills/{billsId}

Returns a single bills, specified by the billsId parameter.

| Name        | Required | Default value |
|-------------|----------|---------------|
| sellersId   | required |               |
| itemsId     | required |               |
| billsId     | required |               |

#### `POST` api/sellers/{sellersId}/items/{itemsId}/bills

Creates a new bills.

| Name      | Required | Default value |
|-----------|----------|---------------|
| sellersId | required |               |
| itemsId   | required |               |

#### `PUT` api/sellers/{sellersId}/items/{itemsId}/bills/{billsId}

Updates a single bills, specified by the billsId parameter.

| Name        | Required | Default value |
|-------------|----------|---------------|
| sellersId   | required |               |
| itemsId     | required |               |
| billsId     | required |               |

#### `DELETE` api/sellers/{sellersId}/items/{itemsid}/bills/{billsId}

Deletes a single bills, specified by the billsId parameter.

| Name        | Required | Default value |
|-------------|----------|---------------|
| sellersId   | required |               |
| itemsId     | required |               |
| billsId     | required |               |

### Auth

#### `POST` api/register

Registers new user

| Name     | Required | Default value |
|----------|----------|---------------|
| Username | required |               |
| Email    | required |               |
| Password | required |               |

#### `POST` api/login

Gets JWT

| Name     | Required | Default value |
|----------|----------|---------------|
| Username | required |               |
| Password | required |               |