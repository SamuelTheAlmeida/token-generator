using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// Iterates over the list of digits and perform N number of rotations.
        /// Encrypts the final rotated list into a Guid token
        /// </summary>
        /// <param name="items">List of digits</param>
        /// <param name="rotations">Rotations to perform</param>
        /// <returns>The generated guid token</returns>
        private async Task<Guid> GenerateTokenAsync(List<int> items, int rotations)
        {
            var result = items;

            for (int i = 0; i < rotations; i++)
                result = await RotateAsync(items);

            return StringToGuid(IntListToString(result));
        }

        /// <summary>
        /// Right circular rotation
        /// Inserts the last item on the beggining of the list, and then right shift all other elements
        /// </summary>
        /// <param name="items">List of integers</param>
        /// <returns>The rotated list of integers</returns>
        private async Task<List<int>> RotateAsync(List<int> items)
        {
            var itemsToTake = items.Count;
            items.Insert(0, items[items.Count - 1]);
            var rotatedList = items.Take(itemsToTake);

            return await Task.FromResult(rotatedList
                .ToList());
        }

        /// <summary>
        /// Encrypts a string into a Guid with sha1.
        /// Uses the hash bytes to generate a Guid.
        /// This Guid will be deterministic
        /// </summary>
        /// <param name="source">source string to be encrypted</param>
        /// <returns>The generated guid</returns>
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

        /// <summary>
        /// Converts an list of integers to a single concatenated string.
        /// Example: [1, 2, 3] -> "122"
        /// </summary>
        /// <param name="items">list of integers to be concatenated</param>
        /// <returns>result string</returns>
        private string IntListToString(List<int> items)
        {
            return string.Join("", items);
        }
    }
}
