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
    [Route("api/sellers/{sellerId}/items")]
    public class ItemController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ISoldProductRepository _oldProductRepository;
        private readonly IAuthorizationService _authorizationService;

        public ItemController(IItemRepository itemRepository, ISellerRepository sellerRepository, ISoldProductRepository sold, IBillRepository billRepository, IAuthorizationService authorizationService)
        {
            _itemRepository = itemRepository;
            _sellerRepository = sellerRepository;
            _billRepository = billRepository;
            _oldProductRepository = sold;
            _authorizationService = authorizationService;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetMany(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetManyAsync(sellerId);
            IEnumerable<ItemDto> result = item.Select(o => new ItemDto(o.id, o.Pavadinimas, o.Aprasymas, o.Kaina, o.Kiekis));
            return result.ToList();
        }


        [HttpGet]
        [Route("{itemId}")]
        public async Task<ActionResult<ItemDto>> Get(int itemId, int sellerId)
        {
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null)
            {
                return NotFound();
            }
            return new ItemDto(item.id, item.Pavadinimas, item.Aprasymas, item.Kaina, item.Kiekis);
        }

        [HttpPost]
        [Authorize (Roles = Roles.Seller)]
        public async Task<ActionResult<ItemDto>> Create(CreateItemDot createItemDot, int sellerId)
        {
            var item = new preke
            {Pavadinimas = createItemDot.Name, Aprasymas = createItemDot.Description, Kaina = createItemDot.Price, Kiekis = createItemDot.Count, fk_PardavejasId = sellerId,
            UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _itemRepository.CreateAsync(item);

            // 201
            return Created("", new ItemDto(item.id, item.Pavadinimas, item.Aprasymas, item.Kaina, item.Kiekis));
        }

        [HttpPut]
        [Route("{itemId}")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult<ItemDto>> Update(int itemId, UpdateItemDot updateitemDto, int sellerId)
        {
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            var authr = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }
            // 404
            if (item == null)
                return NotFound();

            item.Kiekis = updateitemDto.Count;
            item.Kaina = updateitemDto.Price;
            item.Aprasymas = updateitemDto.Description;
            item.Pavadinimas = updateitemDto.Name;
            await _itemRepository.UpdateAsync(item);

            return Ok(new ItemDto(item.id, item.Pavadinimas, item.Aprasymas, item.Kaina, item.Kiekis));
        }

        [HttpDelete("{itemId}", Name = "DeleteItem")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult> Remove(int itemId, int sellerId)
        {
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            var sold = await _oldProductRepository.GetManyAsync(itemId);
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (sold != null)
            {
                await _oldProductRepository.DeleteManyAsync(sold);
            }
            if (seller == null) return NotFound();
            var authr = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }

            // 404
            if (item == null)
                return NotFound();

            await _itemRepository.DeleteAsync(item);


            // 204
            return NoContent();
        }
    }
}