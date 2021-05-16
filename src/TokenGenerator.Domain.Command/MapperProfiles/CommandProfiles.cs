using AutoMapper;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Domain.Command.MapperProfiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            CreateMap<SaveCardCommand, CreateTokenCommand>()
              .ForMember(dest => dest.CardLastFourDigits, 
                        opt => opt.MapFrom(src => src
                        .CardNumber
                        .GetLastDigitsString(4)
                        .StringToIntList())
                        );

            /*CreateMap<ValidateTokenCommand, CreateTokenCommand>()
              .ForMember(dest => dest.CardLastFourDigits,
                        opt => opt.MapFrom(src => src
                        .CardNumber
                        .GetLastDigitsString(4)
                        .StringToIntList())
                        );*/
        }
    }
}
