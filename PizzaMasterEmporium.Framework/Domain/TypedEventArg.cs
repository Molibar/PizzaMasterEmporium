using System;

namespace PizzaMasterEmporium.Framework.Domain
{
    /// <summary>
    /// Encapsulates a single object as a parameter for simple event argument passing
    /// <remarks>Now you can declare a simple event args derived class in one wrap</remarks>
    /// <code>public void delegate MyCustomeEventHandler(TypedEventArg&lt;MyObject&gt; theSingleObjectParameter)</code>
    /// </summary>
    public class TypedEventArg<T> : EventArgs
    {
        public TypedEventArg(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}