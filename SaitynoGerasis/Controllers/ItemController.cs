using Microsoft.AspNetCore.Mvc;
using SaitynoGerasis.Data.Dtos;
using SaitynoGerasis.Data.Entities;
using SaitynoGerasis.Data.Repositories;

namespace SaitynoGerasis.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerController(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
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
        public async Task<ActionResult<SellerDto>> Create(CreateSellerDot createSellerDot)
        {
            var seller = new pardavejas
            { Pavadinimas = createSellerDot.Name, Miestas = createSellerDot.City, Adresas = createSellerDot.address };

            await _sellerRepository.CreateAsync(seller);

            // 201
            return Created("", new SellerDto(seller.id, seller.Pavadinimas, seller.Miestas, seller.Adresas));
        }

        [HttpPut]
        [Route("{sellerId}")]
        public async Task<ActionResult<SellerDto>> Update(int sellerId, UpdateSellerDot updatesellerDto)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);

            // 404
            if (seller == null)
                return NotFound();

            seller.Miestas = updatesellerDto.City;
            seller.Adresas = updatesellerDto.address;
            seller.Pavadinimas = updatesellerDto.Name;
            await _sellerRepository.UpdateAsync(seller);

            return Ok(new SellerDto(seller.id, seller.Pavadinimas, seller.Miestas, seller.Adresas));
        }

        [HttpDelete("{sellerId}", Name = "DeleteSeller")]
        public async Task<ActionResult> Remove(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);

            // 404
            if (seller == null)
                return NotFound();

            await _sellerRepository.DeleteAsync(seller);


            // 204
            return NoContent();
        }
    }
}