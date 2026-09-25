1. Objetivo de la aplicación
El objetivo de esta aplicación es poder guardar y manejar archivos en Azure desde un programa hecho en C#. El programa permite subir, ver, descargar y eliminar archivos.

2.Tecnologías utilizadas
Para realizar el proyecto se utilizó:

C#
.NET
Microsoft Azure
Azure Blob Storage
Visual Studio Code
PowerShell
GitHub

3.Configuración de Azure
En Azure se creó una cuenta de almacenamiento.
Dentro de esa cuenta se creó un contenedor llamado:
archivos
El contenedor se dejó como privado para que los archivos no puedan ser vistos por cualquier persona.

4. Arquitectura de la solución
El funcionamiento es sencillo:
Primero, el usuario utiliza el programa.
Después, el programa se conecta con Azure.
Luego, Azure guarda y administra los archivos dentro del contenedor archivos.
Desde el programa se pueden realizar todas las operaciones.

5. Operaciones de la aplicación

5.1 Subir archivo
El usuario escribe la ubicación de un archivo que tiene en su computadora.
El programa revisa que el archivo exista y luego lo sube a Azure.

5.2 Listar archivos
El programa muestra los archivos que están guardados en Azure.
También muestra el nombre y tamaño de cada archivo.

5.3 Descargar archivo
El usuario escribe el nombre del archivo que quiere descargar.
Luego indica la carpeta donde quiere guardarlo.
El programa descarga el archivo y muestra dónde quedó guardado.

5.4 Eliminar archivo
El usuario escribe el nombre del archivo que quiere eliminar.
Antes de borrarlo, el programa pregunta si está seguro.
Si el usuario confirma, el archivo es eliminado.

6. Manejo de errores
El programa revisa que los datos ingresados sean correctos.
Por ejemplo:
Verifica que el archivo exista antes de subirlo.
Verifica que el archivo exista en Azure antes de descargarlo o eliminarlo.
Verifica que la carpeta de descarga exista.
También muestra un mensaje cuando ocurre algún error.

8. Protección de la Connection String
La Connection String sirve para conectar el programa con la cuenta de Azure.
Como contiene información importante, no se escribió directamente dentro del código.
Se guardó como una variable de entorno en PowerShell.
De esta forma, la clave no queda guardada en el proyecto ni se publica en GitHub.
Instrucciones para ejecutar el proyecto

8.1 Abrir PowerShell.
8.2 Entrar a la carpeta del proyecto.
8.3 Configurar la Connection String.
8.4 Ejecutar el programa con:
dotnet run
8.5 Al iniciar aparecerá el menú:

=================================
     MIA - AZURE BLOB STORAGE
=================================
1. Subir archivo
2. Listar archivos
3. Descargar archivo
4. Eliminar archivo
5. Salir
=================================

8.6 Seleccionar la opción que se desea realizar.
