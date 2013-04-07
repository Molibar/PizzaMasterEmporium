using System.Collections.Generic;

namespace PizzaMasterEmporium.Framework.Generics
{
    /// <summary>
    /// Chain of Responsibility Handler
    /// </summary>
    public abstract class ChainOfRespHandler<T>
    {
        protected ChainOfRespHandler<T> _successor;
        public ChainOfRespHandler<T> Successor { get { return _successor; } set { _successor = value; } }

        public IEnumerable<T> Handle(IEnumerable<T> objects)
        {
            var filtered = Execute(objects);
            if(Successor != null) filtered = Successor.Handle(filtered);
            return filtered;
        }

        protected abstract IEnumerable<T> Execute(IEnumerable<T> objects);
    }
}
