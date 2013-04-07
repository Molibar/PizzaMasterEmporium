using System;

namespace PizzaMasterEmporium.Framework.Domain
{
    public interface IServiceRequest<TEventArgs, TTarget>
    {
        void Execute(TEventArgs input);
        event EventHandler<TypedEventArg<TTarget>> ServiceCompleted;
        object State { get; set; }
    }
}