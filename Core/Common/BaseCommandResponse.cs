using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class BaseCommandResponse<T>
    {
        public BaseCommandResponse()
        {
        }

        public BaseCommandResponse(string message)
        {
            Errors = new List<string>();
            Message = message;
            Success = true;
        }
        public BaseCommandResponse(string message, T model)
        {
            Errors = new List<string>();
            Message = message;
            Success = true;
            Object = model;
        }
        public BaseCommandResponse(string message,bool success)
        {
            Errors = new List<string>();
            Message = message;
            Success = success;
        }

        public BaseCommandResponse(string message, List<string> errors = null, bool success = false)
        {
            Errors = errors ?? new List<string>();
            Message = message;
            Success = success;
        }

        public int Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Object { get; set; }
    }
}
