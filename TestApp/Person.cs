using System;
namespace TestApp
{
    public class Person
    {
        public Team Team { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;
    }
}