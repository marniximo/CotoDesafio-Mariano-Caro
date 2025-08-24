Este es el repositorio de git para el desafio tecnico del Coto
Se realiza un servicio REST para la venta de automoviles con DDD y CQRS como fueron mencionados en la entrevista

El servicio esta configurado para funcionar con una base de datos InMemory de SQLite. Se hizo pensando en que a futuro se puede cambiar facilmente a una base de datos SQL persistente con pocos cambios de codigo.



Se detalla la forma de uso del servicio a continuacion:

POST: Se puede registrar una venta hacienda un POST a esta ruta. Se deben proveer el modelo del auto y el id del centro de distribucion solamente

GET GetTotalSalesByCenterId: Se pueden obtener el total de cantidad de ventas y de recaudacion de un centro de ventas dado el ID del mismo
GET GetTotalSales: Se pueden obtener el total de cantidad de ventas y de recaudacion de todos los centros de ventas

GET GetPercentages: Se pueden obtener el porcentaje de ventas con respecto al volumen de ventas total (cantidad) de la empresa, agrupado por centro de distribucion y luego por modelo de auto. El porcentaje de devuelve como un flotante con 2 posiciones decimales, sin signo de %.



Al ejecutar el servicio se cuenta con Swagger de todas formas, donde se encontrara informacion mas detallada acerca de los requests y responses

