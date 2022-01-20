using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Common
{
    public class Person
    {
        private int _age;
        private string _name;

        public Person(int age, string name)
        {
            _age = age;
            _name = name;
        }

        public override string ToString()
        {
            return $"{_name} is {_age} years old";
        }
    }
}
