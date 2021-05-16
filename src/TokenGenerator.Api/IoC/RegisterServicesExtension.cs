using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Command.ValidateToken;
using TokenGenerator.Infrastructure.Data.Repositories;

namespace TokenGenerator.Api.IoC
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient<IValidator<SaveCardCommand>, SaveCardCommandValidator>();

            services.AddScoped<ISaveCardCommandHandler, SaveCardCommandHandler>();
            services.AddScoped<ICreateTokenCommandHandler, CreateTokenCommandHandler>();
            services.AddScoped<IValidateTokenCommandHandler, ValidateTokenCommandHandler>();

            services.AddScoped<ICardRepository, CardRepository>();
        }
    }
}
