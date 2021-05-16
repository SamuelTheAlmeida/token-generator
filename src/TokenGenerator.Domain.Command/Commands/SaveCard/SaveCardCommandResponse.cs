using System;

namespace TokenGenerator.Domain.Command.Commands.SaveCard
{
    public class SaveCardCommandResponse
    {
        public DateTime CreationDate { get; set; }
        public Guid Token { get; set; }
        public Guid CardId { get; set; }

        public SaveCardCommandResponse(
            Guid token,
            Guid cardId)
        {
            CreationDate = DateTime.Now;
            Token = token;
            CardId = cardId;
        }
    }
}
