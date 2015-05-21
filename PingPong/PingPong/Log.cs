using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public enum Log_Level {

        DEBUG,
        LOG

    }

    class Log
    {

        public const Log_Level LOG_LEVEL = Log_Level.DEBUG;

        public static void log(object obj)
        {
            Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj);
        }

        public static void log(object obj, object obj0)
        {
            Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0);
        }

        public static void log(object obj, object obj0, object obj1)
        {
            Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1);
        }

        public static void log(object obj, object obj0, object obj1, object obj2)
        {
            Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1, obj2);
        }

        public static void log(object obj, object obj0, object obj1, object obj2, object obj3)
        {
            Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1, obj2, obj3);
        }

        public static void debug(object obj)
        {
            if (LOG_LEVEL.Equals(Log_Level.DEBUG)) Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj);
        }

        public static void debug(object obj, object obj0)
        {
            if (LOG_LEVEL.Equals(Log_Level.DEBUG)) Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0);
        }

        public static void debug(object obj, object obj0, object obj1)
        {
            if (LOG_LEVEL.Equals(Log_Level.DEBUG)) Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1);
        }

        public static void debug(object obj, object obj0, object obj1, object obj2)
        {
            if (LOG_LEVEL.Equals(Log_Level.DEBUG)) Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1, obj2);
        }

        public static void debug(object obj, object obj0, object obj1, object obj2, object obj3)
        {
            if (LOG_LEVEL.Equals(Log_Level.DEBUG)) Console.WriteLine(DateTime.Now.ToString() + " [PingPong]: " + obj, obj0, obj1, obj2, obj3);
        }

    }
}
