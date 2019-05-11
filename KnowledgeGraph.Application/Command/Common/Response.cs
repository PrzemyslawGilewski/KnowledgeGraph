using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application
{
    public class Response<T>
    {
        public string Message { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public T ResponseObject { get; protected set; }

        public static Response<T> Ok(T responseObject)
        {
            return new Response<T> { IsSuccess = true, ResponseObject = responseObject };
        }

        public static Response<T> Fail(string message)
        {
            return new Response<T> { IsSuccess = false, Message = message };
        }
    }
}
