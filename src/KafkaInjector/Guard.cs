using System;

namespace KafkaInjector
{
    public static class Guard
    {
        public static void ArgumentNotNull<T>(T value, string name)
        {
            if (value == null || value.Equals(default(T)))
            {
                throw new ArgumentNullException(name, "cannot be null");
            }
        }
    }
}