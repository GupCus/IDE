// See https://aka.ms/new-console-template for more information
Console.Write("Ingrese Texto: ");
string inputTexto = Console.ReadLine();

if (string.IsNullOrEmpty(inputTexto)) {
    Console.WriteLine("El texto es nulo o vacío");
}
else
{
    ConsoleKeyInfo opcion = default;
    Console.WriteLine("Elija una opción:");
    Console.WriteLine("1. Convertir a mayúsculas");
    Console.WriteLine("2. Convertir a minúsculas");
    Console.WriteLine("3. Obtener longitud del texto");
    Console.Write("Opción: ");
    opcion = Console.ReadKey();
    Console.WriteLine("");

    /* Caso 1
        if (opcion.Key == ConsoleKey.D1)
        {
            Console.WriteLine(inputTexto.ToUpper());
        }
        else if (opcion.Key == ConsoleKey.D2)
        {
            Console.WriteLine(inputTexto.ToLower());
        }
        else if (opcion.Key == ConsoleKey.D3)
        {
            Console.WriteLine(inputTexto.Length);
        }
        else
        {
            Console.WriteLine("Lo ingresado no es 1, 2 o 3");
        }
    */

    /* Caso 2
    switch (opcion.Key)
    {
        case ConsoleKey.D1:
            Console.WriteLine(inputTexto.ToUpper());
            break;
        case ConsoleKey.D2:
            Console.WriteLine(inputTexto.ToLower());
            break;
        case ConsoleKey.D3:
            Console.WriteLine(inputTexto.Length);
            break;
        default:
            Console.WriteLine("Lo ingresado no es 1, 2 o 3");
            break;
    }
    */
}

