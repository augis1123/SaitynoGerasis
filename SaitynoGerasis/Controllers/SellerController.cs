using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SaitynoGerasis.Auth.Model;
using SaitynoGerasis.Data.Dtos;
using SaitynoGerasis.Data.Entities;
using SaitynoGerasis.Data.Repositories;
using System.Security.Claims;

namespace SaitynoGerasis.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellerController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ISoldProductRepository _oldProductRepository;
        private readonly IAuthorizationService _authorizationService;

        public SellerController(ISellerRepository sellerRepository, IBillRepository billRepository, IItemRepository itemRepository, ISoldProductRepository sold, IAuthorizationService authorizationService)
        {
            _sellerRepository = sellerRepository;
            _billRepository = billRepository;
            _itemRepository = itemRepository;
            _oldProductRepository = sold;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<SellerDto>> GetMany()
        {
            var sellers = await _sellerRepository.GetManyAsync();
            return sellers.Select(o => new SellerDto(o.id, o.Pavadinimas, o.Miestas, o.Adresas));
        }


        [HttpGet]
        [Route("{sellerId}")]
        public async Task<ActionResult<SellerDto>> Get(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null)
            {
                return NotFound();
            }
            return new SellerDto(seller.id, seller.Pavadinimas, seller.Miestas, seller.Adresas);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult<SellerDto>> Create(CreateSellerDot createSellerDot)
        {
            var seller = new pardavejas
            { Pavadinimas = createSellerDot.Name, Miestas = createSellerDot.City, Adresas = createSellerDot.address,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            var authr = await _authorizationService.AuthorizeAsync(User, seller, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }
            await _sellerRepository.CreateAsync(seller);

            // 201
            return Created("", new SellerDto(seller.id, seller.Pavadinimas, seller.Miestas, seller.Adresas));
        }

        [HttpPut]
        [Route("{sellerId}")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult<SellerDto>> Update(int sellerId, UpdateSellerDot updatesellerDto)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);

            // 404
            if (seller == null)
                return NotFound();
            var authr = await _authorizationService.AuthorizeAsync(User, seller, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }
            seller.Miestas = updatesellerDto.City;
            seller.Adresas = updatesellerDto.address;
            seller.Pavadinimas = updatesellerDto.Name;
            await _sellerRepository.UpdateAsync(seller);
            return Ok(new SellerDto(seller.id, seller.Pavadinimas, seller.Miestas, seller.Adresas));
        }

        [HttpDelete("{sellerId}", Name = "DeleteSeller")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult> Remove(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);
            var authr = await _authorizationService.AuthorizeAsync(User, seller, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }

            var items = await _itemRepository.GetManyAsync(sellerId);

            if(items != null)
            {
                await _oldProductRepository.DeleteManyAsyncByItems(items);
                await _itemRepository.DeleteManyAsync(items);
            }

            // 404
            if (seller == null)
                return NotFound();

            await _sellerRepository.DeleteAsync(seller);


            // 204
            return NoContent();
        }
    }
}
