using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities._585.Models
{
    public class Reply
    {
        public bool IsSuccess { get; }
        public IEnumerable<string>? Errors { get; }

        public Reply(bool isSuccess, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Reply Success() => new(true);
        public static Reply Fail() => new(false);
        public static Reply Fail(string error) => new(false, [error]);
        public static Reply Fail(IEnumerable<string> errors) => new(false, errors);
    }

    public class Reply<T> : Reply
    {
        public T? Data { get; }
        public Reply(bool isSuccess, IEnumerable<string>? errors = null, T? data = default) : base(isSuccess, errors)
        {
            Data = data;
        }

        public static Reply<T> Success(T data) => new(true, null, data);
        public static new Reply<T> Fail() => new(false);
        public static new Reply<T> Fail(string error) => new(false, [error]);
        public static new Reply<T> Fail(IEnumerable<string> errors) => new(false, errors);
    }
}
