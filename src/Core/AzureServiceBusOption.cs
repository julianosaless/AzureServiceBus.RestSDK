using System;

namespace AzureServiceBus.RestSDK.Core
{
    public struct AzureServiceBusOption<T>
    {
        internal T Value { get; }
        public bool IsSome { get; }
        public bool IsNone => !IsSome;

        internal AzureServiceBusOption(T value, bool isSome)
        {
            Value = value;
            IsSome = isSome;
        }

        public TR Match<TR>(Func<T, TR> some, Func<TR> none)
            => IsSome ? some(Value) : none();

        public static readonly AzureServiceBusOption<T> None = new AzureServiceBusOption<T>();

    }

    public struct AzureServiceBusTry<TFailure, TSuccess>
    {
        internal TFailure Failure { get; }
        internal TSuccess Success { get; }

        public bool IsFailure { get; }
        public bool IsSucess => !IsFailure;


        internal AzureServiceBusTry(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default;
        }

        internal AzureServiceBusTry(TSuccess success)
        {
            IsFailure = false;
            Failure = default;
            Success = success;
        }

        public TResult Match<TResult>(
                Func<TFailure, TResult> failure,
                Func<TSuccess, TResult> success
            )
            => IsFailure ? failure(Failure) : success(Success);


        public static implicit operator AzureServiceBusTry<TFailure, TSuccess>(TFailure failure)
            => new AzureServiceBusTry<TFailure, TSuccess>(failure);

        public static implicit operator AzureServiceBusTry<TFailure, TSuccess>(TSuccess success)
            => new AzureServiceBusTry<TFailure, TSuccess>(success);

        public static AzureServiceBusTry<TFailure, TSuccess> Of(TSuccess obj) => obj;
        public static AzureServiceBusTry<TFailure, TSuccess> Of(TFailure obj) => obj;
    }
}
