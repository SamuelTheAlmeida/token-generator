using System.ComponentModel;

namespace TokenGenerator.Domain.Enums
{
    public enum DefaultResults
    {
        [Description("An unknown error has ocurred. Try again later or contact the technical support.")]
        InternalError = 99,

        [Description("Operation has performed successfully")]
        Success = 1,

        [Description("One or more validation errors occurred")]
        ValidationErrors = 2,
    }
}
