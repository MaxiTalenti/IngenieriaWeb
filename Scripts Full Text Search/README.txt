Estos scripts corresponden a la imprementaci�n de B�squedas full text search.

Nuestra idea era trabajar la b�squeda con dos vistas distintas, y luego dentro de un sp hacer un union de ambas. En una vista realizar la 
b�squeda de eventos e informacion relacionada que pudiesemos sacar de aqu� (especificamente detalle de eventos, direccion y Usuarios). El problema era
que si lo trabajabamos de �sta forma nunca ibamos a poder obtener resultados, por ejemplo, si buscabamos el nombre de un usuario que jamas 
haya creado un evento (al tener inner join estos usuarios no aparecerian en la lista). Al notar esto decidimos dividir informacion de 
evento en una vista y Usuarios en otra. 
En la otra vista que queriamos hacer originalmente buscariamos comentarios de eventos. El problema es que para poder trabajar con varias vistas
es necesario crear m�s cat�logos. 
En este momento tenemos 3 catalogos con sus respectivas vistas (uno para busqueda de eventos, otro para usuarios y otro para comentarios). En 
nuestras m�quinas, que tenemos instalado SQL Server, la combinacion de los resultados de las vistas funcionan bien. El inconveniente que surge en
el servidor es que las vistas full text (utilizando FREETEXT) de comentarios y usuarios no traen resultados. Al hacer un select de las vistas
los datos estan estan correctos, pero al aplicar FREETEXT en el Where para poder filtrar no arroja ningun resultado. 
El motor de base de datos de App Harbor es version SQL Express, quiz�s no soporta la funcionalidad de trabajar con m�s de un cat�logo.
