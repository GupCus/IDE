using Clases;

B b = new B();
A a = b;
a.F();
b.F();
a.G();
b.G();

Console.ReadKey();

/* 6) Salida:
    A.F
    B.F
    B.G
    B.G */