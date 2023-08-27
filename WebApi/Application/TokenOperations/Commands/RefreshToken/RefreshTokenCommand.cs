using AutoMapper;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.TokenOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string _refreshToken { get; set; }
        private readonly IPaintingStoreDbContext _context;
        private IConfiguration _configuration { get; set; }

        public RefreshTokenCommand(IPaintingStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var customer = _context.Customers.FirstOrDefault(x => x.RefreshToken == _refreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(customer != null)
            {
                // Create Token
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccesToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("No valid refresh token found!");
        }
    }
}