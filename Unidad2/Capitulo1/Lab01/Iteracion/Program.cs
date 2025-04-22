// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
int cantIteraciones = 5;
string[] arreglo = new string[cantIteraciones];

for(int i=0; i< cantIteraciones; i++)
{
    Console.Write("Ingrese un nombre: ");
    arreglo[i] = Console.ReadLine();
    Console.WriteLine("");
};

Console.WriteLine("Los nombres ingresados son: ");
for (int i = 0; i < cantIteraciones; i++)
{
    Console.WriteLine(arreglo[^(i+1)]);
}
;