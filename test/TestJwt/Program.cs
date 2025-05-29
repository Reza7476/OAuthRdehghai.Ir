using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

class JwtSignatureTest
{
    static void Main()
    {
        // کلید تستی را اینجا بگذار
        var key = "Reza1420abcdefgtrwenvdfratyevshakidegdvs1423";

        // پارامترها
        var issuer = "Oauth.rdehghai.ir";
        var audience = "oauth.front.rdehghai.ir";



        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Site", audience),
            new Claim("Phone_Number","4155"),
            new Claim("FirstName","adsfas"),
            new Claim("LastName","sdfvsd")
        };



        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Oauth.rdehghai.ir",
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(20),
            signingCredentials: credentials);

        var handlerTest=new  JwtSecurityTokenHandler();

        var inputToken = new JwtSecurityTokenHandler().WriteToken(token);

        if (string.IsNullOrEmpty(issuer))
        {
            throw new Exception("issuer null");
        }
        else
        {

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidIssuer = issuer,
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                //ClockSkew = TimeSpan.Zero
            };

            if (!string.IsNullOrWhiteSpace(inputToken))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var principal = handler.ValidateToken(inputToken, validationParameters, out var validatedToken);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅signature and exp of token is valid !");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ signature or validation of token is invalid !");
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        Console.WriteLine("end.");
    }
}