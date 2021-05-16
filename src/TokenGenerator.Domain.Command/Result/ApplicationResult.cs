using FluentValidation.Results;
using System.Collections.Generic;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Enums;

namespace TokenGenerator.Domain.Command.Result
{
    public class ApplicationResult<T>
    {
        public ApplicationResult()
        {
            Data = default;
        }

        public ApplicationResult(bool success, DefaultResults message)
        {
            Success = success;
            Message = new EnumModel
            {
                Code = message.GetEnumValue(),
                Name = message.GetEnumName(),
                Description = message.GetEnumDescription()
            };
            Data = default;
        }

        public ApplicationResult(bool success, DefaultResults message, T data) : this(success, message) 
        {
            Data = data;
        }

        public ApplicationResult(bool success, DefaultResults message, List<ValidationFailure> validationErrors) : this(success, message)
        {
            ValidationErrors = validationErrors;
        }

        public T Data { get; set; }
        public EnumModel Message { get; set; }
        public bool Success { get; set; }
        public List<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
    }

    public partial class ApplicationResult : ApplicationResult<dynamic>
    {
        public ApplicationResult() : base()
        {

        }

        public ApplicationResult(bool success, DefaultResults message) : base(success, message)
        {

        }

        public ApplicationResult(bool success, DefaultResults message, dynamic data) : base(success, message) => Data = data;
    }
}
