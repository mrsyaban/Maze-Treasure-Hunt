using System;

struct MyClass
{
    public int x;
    public int y;
}

class Program
{
    static void Main(string[] args)
    {
        MyClass obj = new MyClass();
        obj.x = 10;
        obj.y = 20;


        int ptr = &obj;
        Console.WriteLine("{0}", ptr);
    }
}