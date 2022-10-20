using Microsoft.AspNetCore.Mvc;
using SaitynoGerasis.Data.Dtos;
using SaitynoGerasis.Data.Entities;
using SaitynoGerasis.Data.Repositories;

namespace SaitynoGerasis.Controllers
{
    [ApiController]
    [Route("api/sellers/{sellerId}/items/{itemId}/bills")]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ISoldProductRepository _oldProductRepository;

        public BillController(IBillRepository billRepository, IItemRepository itemRepository, ISellerRepository seller, ISoldProductRepository soldProductRepository)
        {
            _billRepository = billRepository;
            _itemRepository = itemRepository;
            _sellerRepository = seller;
            _oldProductRepository = soldProductRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDto>>> GetMany(int itemId, int sellerId)
        {
            var sold = await _oldProductRepository.GetManyAsync(itemId);
            if (sold == null) return NotFound();
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null) return NotFound();

            var bill = await _billRepository.GetManyAsync(sold);
            return bill.Select(o => new BillDto(o.Id, o.Vardas, o.Pavarde, o.Miestas, o.Adresas, o.PirkimoData)).ToList();
        }


        [HttpGet]
        [Route("{billId}")]
        public async Task<ActionResult<BillDto>> Get(int billId, int sellerId, int itemId)
        {
            var sold = await _oldProductRepository.GetAsync(itemId, billId);
            if (sold == null) return NotFound();
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null) return NotFound();

            var bill = await _billRepository.GetAsync(billId);
            if (bill == null)
            {
                return NotFound();
            }
            return new BillDto(bill.Id, bill.Vardas, bill.Pavarde, bill.Miestas, bill.Adresas, bill.PirkimoData);
        }

        [HttpPost]
        public async Task<ActionResult<BillDto>> Create(CreateBillDot createBillDot)
        {
            var bill = new saskaita
            { Id = createBillDot.id, Vardas = createBillDot.BuyerName, Pavarde = createBillDot.BuyerSecondName, Miestas = createBillDot.city, Adresas = createBillDot.address, PirkimoData = createBillDot.DateTime};

            await _billRepository.CreateAsync(bill);

            // 201
            return Created("", new BillDto(bill.Id, bill.Vardas, bill.Pavarde, bill.Miestas, bill.Adresas, bill.PirkimoData));
        }

        [HttpPut]
        [Route("{billId}")]
        public async Task<ActionResult<BillDto>> Update(int billId, UpdateBillDot updatebillDto)
        {
            var bill = await _billRepository.GetAsync(billId);

            // 404
            if (bill == null)
                return NotFound();

            bill.Vardas = updatebillDto.BuyerName;
            bill.Pavarde = updatebillDto.BuyerSecondName;
            bill.Miestas = updatebillDto.city;
            bill.Adresas = updatebillDto.address;

            await _billRepository.UpdateAsync(bill);

            return Ok(new BillDto(bill.Id, bill.Vardas, bill.Pavarde, bill.Miestas, bill.Adresas, bill.PirkimoData));
        }

        [HttpDelete("{billId}", Name = "DeleteBill")]
        public async Task<ActionResult> Remove(int billId)
        {
            var bill = await _billRepository.GetAsync(billId);

            // 404
            if (bill == null)
                return NotFound();

            await _billRepository.DeleteAsync(bill);


            // 204
            return NoContent();
        }
    }
}