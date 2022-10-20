using Microsoft.AspNetCore.Mvc;
using SaitynoGerasis.Data.Dtos;
using SaitynoGerasis.Data.Entities;
using SaitynoGerasis.Data.Repositories;

namespace SaitynoGerasis.Controllers
{
    [ApiController]
    [Route("api/sellers/{sellerId}/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ISellerRepository _sellerRepository;

        public ItemController(IItemRepository itemRepository, ISellerRepository sellerRepository)
        {
            _itemRepository = itemRepository;
            _sellerRepository = sellerRepository;
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
        public async Task<ActionResult<ItemDto>> Create(CreateItemDot createItemDot, int sellerId)
        {
            var item = new preke
            {Pavadinimas = createItemDot.Name, Aprasymas = createItemDot.Description, Kaina = createItemDot.Price, Kiekis = createItemDot.Count, fk_PardavejasId = sellerId};

            await _itemRepository.CreateAsync(item);

            // 201
            return Created("", new ItemDto(item.id, item.Pavadinimas, item.Aprasymas, item.Kaina, item.Kiekis));
        }

        [HttpPut]
        [Route("{itemId}")]
        public async Task<ActionResult<ItemDto>> Update(int itemId, UpdateItemDot updateitemDto, int sellerId)
        {
            var item = await _itemRepository.GetAsync(itemId, sellerId);

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
        public async Task<ActionResult> Remove(int itemId, int sellerId)
        {
            var item = await _itemRepository.GetAsync(itemId, sellerId);

            // 404
            if (item == null)
                return NotFound();

            await _itemRepository.DeleteAsync(item);


            // 204
            return NoContent();
        }
    }
}