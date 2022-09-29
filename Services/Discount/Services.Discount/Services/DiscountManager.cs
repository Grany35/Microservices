
using Dapper;
using Microservice.Shared.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace Services.Discount.Services
{
    public class DiscountManager : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return status;
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * from Discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 204);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from Discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var discount = discounts.FirstOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("discount not found", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * from Discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<int> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("Insert into Discount (userid,rate,code) values(@UserId,@Rate,@Code)", discount);

            return saveStatus;

        }

        public async Task<int> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update Discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", discount);

            return status;
        }
    }
}
