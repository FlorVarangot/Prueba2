Trabajo Práctico Cuatrimestral - Programación III - UTN FRGP 2024
Equipo 26: María Florencia Rodríguez Varangot - Matias Federico Velazquez Segovia

APLICACIÓN PARA NEGOCIO DE ARTÍCULOS DE LIBRERÍA - (MOSTRADOR)

OBJETIVO:
El objetivo principal de la aplicación es administrar las compras y ventas de un negocio de artículos de librería.
El sistema permitirá administrar artículos, marcas, categorías, proveedores y clientes, y registrar compras y ventas.
Por cada COMPRA (a un proveedor) se registrará el precio de costo del artículo, y se calculará su precio de venta sumando la multiplicación por el porcentaje de ganancia ingresado para cada artículo. Por cada VENTA (a un cliente) se registrará el precio de venta unitario y el total, que resultará de la suma del último precio de compra con ganancia, multiplicada por las cantidades seleccionadas para cada artículo. 

Los artículos serán discriminados por categorías, y estarán asociados a una marca, que a su vez estará asociada a un proveedor. Además contarán con un stock disponible y un stock mínimo, a tener en cuenta para proyectar las compras y condicionar el registro de venta, en caso de que el stock sea insuficiente. Habrá un aviso 
En el registro de compras y ventas se podrá consultar el histórico de operaciones realizadas para cada proveedor y para cada cliente.

Perfiles:
- Un usuario con permisos de VENDEDOR/A podrá registrar y consultar las compras y ventas realizadas, registrar nuevos clientes y ver los artículos.
- El perfil ADMINISTRADOR/A contará con permisos totales. Sumará al perfil vendedor/a la posibilidad de: dar de alta nuevos artículos, marcas, categorías y proveedores, y realizar nuevas asociaciones o modificar las existentes.

Correos:
* Para cada registro nuevo se enviará un mail de confirmación de registro.
* Para cada venta confirmada se enviará un mail al cliente con el detalle de la venta.
* Cuando el stock disponible de un artículo alcance 1 o 0 luego de una venta, se enviará un mail de recordatorio de compra al vendedor/a involucrado/a en la venta con copia al encargado/a.
