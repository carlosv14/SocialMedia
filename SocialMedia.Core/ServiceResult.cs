using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Enum;

namespace SocialMedia.Core
{
    public class ServiceResult<T>
    {
        public ServiceResult(ResponseCode responseCode, string error, T result)
        {
            ResponseCode = responseCode;
            Error = error;
            Result = result;
        }

        public ResponseCode ResponseCode { get; }
        public string Error { get; }
        public T Result { get; }

        public static ServiceResult<T> ErrorResult(string error) =>
            new ServiceResult<T>(ResponseCode.Error, error, default(T));

        public static ServiceResult<T> SuccessResult(T entity) =>
            new ServiceResult<T>(ResponseCode.Error, string.Empty, entity);

        public static ServiceResult<T> NotFoundResult(string error) =>
            new ServiceResult<T>(ResponseCode.NotFound, error, default(T));
    }
}
