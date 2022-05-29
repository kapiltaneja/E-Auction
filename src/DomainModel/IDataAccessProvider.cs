using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Model;

namespace DomainModel
{
    public interface IDataAccessProvider
    {
       
        Task<SellerInfo> AddSeller(SellerInfo sellerRecord);

        Task<List<SellerInfo>> GetAllSeller();

        Task<ProductInfo> AddProduct(ProductInfo productRecord);

        Task<List<ProductInfo>> GetAllProducts();

        Task<BuyerInfo> AddBuyer(BuyerInfo buyerRecord);

        Task<List<BuyerInfo>> GetAllBuyer();

        Task UpdateBid(int productId, string buyerEmailId, double newBidAmt);

        Task<ProductInfo> GetProductById(int productId);

        Task<List<BuyerInfo>> GetAllBidsByProductId(int productId);

        Task<bool> ExistsProducts(long id);
        Task DeleteProduct(long productId);
    }
}
