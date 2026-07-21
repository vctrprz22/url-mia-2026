using System.Text.Json;
string[] lineas = File.ReadAllLines("estudiantes.csv");
List<Estudiante> estudiantes = new List<Estudiante>();
for (int i = 1; i < lineas.Length; i++)
{
    string[] datos = lineas[i].Split(',');

    Estudiante estudiante = new Estudiante();

    estudiante.Id = int.Parse(datos[0]);
    estudiante.Nombre = datos[1];
    estudiante.Carrera = datos[2];

    estudiantes.Add(estudiante);
}

for (int i = 0; i < estudiantes.Count; i++)
{
    Console.WriteLine(
        estudiantes[i].Id + " - " +
        estudiantes[i].Nombre + " - " +
        estudiantes[i].Carrera
    );
}
string json = JsonSerializer.Serialize(estudiantes);

File.WriteAllText("estudiantes.json", json);

Console.WriteLine("Archivo estudiantes.json creado correctamente.");
