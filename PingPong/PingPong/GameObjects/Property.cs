using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong.GameObjects
{

    public abstract class Property
    {
        public String name;
    }

    public class Property<T>: Property
    {
        T _value;

        public Property(String name, T value)
        {
            this._value = value;
            this.name = name;
        }

        public Property(String name)
        {
            this.name = name;
        }

        public T get() 
        {
            return _value;
        }

        public void set(T value)
        {
            this._value = value;
        }

    }
}
