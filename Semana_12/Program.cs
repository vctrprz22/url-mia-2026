using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string? connectionString =
    Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

string containerName = "archivos";

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("ERROR: No se encontro la Connection String.");
    return;
}

BlobServiceClient blobServiceClient =
    new BlobServiceClient(connectionString);

BlobContainerClient containerClient =
    blobServiceClient.GetBlobContainerClient(containerName);

await containerClient.CreateIfNotExistsAsync(
    PublicAccessType.None
);

int opcion = 0;

do
{
    Console.Clear();

    Console.WriteLine("=================================");
    Console.WriteLine("     MIA - AZURE BLOB STORAGE");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Subir archivo");
    Console.WriteLine("2. Listar archivos");
    Console.WriteLine("3. Descargar archivo");
    Console.WriteLine("4. Eliminar archivo");
    Console.WriteLine("5. Salir");
    Console.WriteLine("=================================");
    Console.Write("Seleccione una opcion: ");

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.WriteLine("Opcion no valida.");
        Console.ReadKey();
        continue;
    }

    switch (opcion)
    {
        case 1:
            await SubirArchivo(containerClient);
            break;

        case 2:
            await ListarArchivos(containerClient);
            break;

        case 3:
            await DescargarArchivo(containerClient);
            break;

        case 4:
            await EliminarArchivo(containerClient);
            break;

        case 5:
            Console.WriteLine("Saliendo...");
            break;

        default:
            Console.WriteLine("Opcion no valida.");
            break;
    }

    if (opcion != 5)
    {
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

} while (opcion != 5);


// SUBIR ARCHIVO
static async Task SubirArchivo(
    BlobContainerClient containerClient)
{
    try
    {
        Console.Write("Ingrese la ruta del archivo: ");
        string? ruta = Console.ReadLine();

        if (string.IsNullOrEmpty(ruta))
        {
            Console.WriteLine("La ruta esta vacia.");
            return;
        }

        ruta = ruta.Trim('"');

        if (!File.Exists(ruta))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }

        string nombreArchivo = Path.GetFileName(ruta);

        BlobClient blobClient =
            containerClient.GetBlobClient(nombreArchivo);

        await blobClient.UploadAsync(
            ruta,
            overwrite: true
        );

        Console.WriteLine();
        Console.WriteLine("Archivo subido correctamente.");
        Console.WriteLine("Nombre: " + nombreArchivo);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al subir archivo:");
        Console.WriteLine(ex.Message);
    }
}


// LISTAR ARCHIVOS
static async Task ListarArchivos(
    BlobContainerClient containerClient)
{
    try
    {
        Console.WriteLine();
        Console.WriteLine("Nombre                         Tamano");
        Console.WriteLine("--------------------------------------------");

        bool hayArchivos = false;

        await foreach (
            BlobItem blobItem
            in containerClient.GetBlobsAsync())
        {
            hayArchivos = true;

            long tamano =
                blobItem.Properties.ContentLength ?? 0;

            Console.WriteLine(
                $"{blobItem.Name,-30} {tamano} bytes"
            );
        }

        if (!hayArchivos)
        {
            Console.WriteLine("No hay archivos.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al listar:");
        Console.WriteLine(ex.Message);
    }
}


// DESCARGAR ARCHIVO
static async Task DescargarArchivo(
    BlobContainerClient containerClient)
{
    try
    {
        Console.Write("Ingrese el nombre del blob: ");
        string? nombreBlob = Console.ReadLine();

        if (string.IsNullOrEmpty(nombreBlob))
        {
            Console.WriteLine("Nombre invalido.");
            return;
        }

        BlobClient blobClient =
            containerClient.GetBlobClient(nombreBlob);

        if (!await blobClient.ExistsAsync())
        {
            Console.WriteLine("El archivo no existe en Azure.");
            return;
        }

        Console.Write("Ingrese la carpeta de destino: ");
        string? carpeta = Console.ReadLine();

        if (string.IsNullOrEmpty(carpeta))
        {
            Console.WriteLine("Carpeta invalida.");
            return;
        }

        carpeta = carpeta.Trim('"');

        if (!Directory.Exists(carpeta))
        {
            Console.WriteLine("La carpeta no existe.");
            return;
        }

        string rutaDestino =
            Path.Combine(carpeta, nombreBlob);

        await blobClient.DownloadToAsync(rutaDestino);

        Console.WriteLine();
        Console.WriteLine("Archivo descargado correctamente.");
        Console.WriteLine("Guardado en:");
        Console.WriteLine(rutaDestino);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al descargar:");
        Console.WriteLine(ex.Message);
    }
}


// ELIMINAR ARCHIVO
static async Task EliminarArchivo(
    BlobContainerClient containerClient)
{
    try
    {
        Console.Write("Ingrese el nombre del blob: ");
        string? nombreBlob = Console.ReadLine();

        if (string.IsNullOrEmpty(nombreBlob))
        {
            Console.WriteLine("Nombre invalido.");
            return;
        }

        BlobClient blobClient =
            containerClient.GetBlobClient(nombreBlob);

        if (!await blobClient.ExistsAsync())
        {
            Console.WriteLine("El archivo no existe en Azure.");
            return;
        }

        Console.Write(
            "Esta seguro que desea eliminarlo? (S/N): "
        );

        string? respuesta = Console.ReadLine();

        if (respuesta?.ToUpper() != "S")
        {
            Console.WriteLine("Operacion cancelada.");
            return;
        }

        await blobClient.DeleteIfExistsAsync();

        Console.WriteLine();
        Console.WriteLine("Archivo eliminado correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al eliminar:");
        Console.WriteLine(ex.Message);
    }
}