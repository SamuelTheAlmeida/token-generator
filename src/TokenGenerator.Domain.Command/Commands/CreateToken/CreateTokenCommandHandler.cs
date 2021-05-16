using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Interfaces;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;

namespace TokenGenerator.Domain.Command.CreateToken
{
    public class CreateTokenCommandHandler : ICreateTokenCommandHandler
    {

        public CreateTokenCommandHandler()
        {

        }

        public async Task<Guid> HandleAsync(CreateTokenCommand command)
        {
            return await GenerateTokenAsync(command.CardLastFourDigits, command.Cvv);
        }

        private async Task<Guid> GenerateTokenAsync(List<int> items, int rotations)
        {
            var result = items;

            for (int i = 0; i < rotations; i++)
                result = await RotateArrayAsync(items);

            return StringToGuid(IntListToString(result));
        }

        private async Task<List<int>> RotateArrayAsync(List<int> items)
        {
            var itemsToTake = items.Count;
            items.Insert(0, items[items.Count - 1]);
            var rotatedList = items.Take(itemsToTake);

            return await Task.FromResult(rotatedList
                .ToList());
        }


        private string IntListToString(List<int> items)
        {
            return string.Join("", items);
        }

        private Guid StringToGuid(string source)
        {
            var guidByteArrayLength = 16;

            var stringBytes = Encoding.UTF8.GetBytes(source);
            var hashedBytes = new System.Security.Cryptography
                .SHA1CryptoServiceProvider()
                .ComputeHash(stringBytes);

            Array.Resize(ref hashedBytes, guidByteArrayLength);
            return new Guid(hashedBytes);
        }
    }
}
