using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SaitynoGerasis.Auth.Model;
using SaitynoGerasis.Data.Dtos;
using SaitynoGerasis.Data.Entities;
using SaitynoGerasis.Data.Repositories;
using System.Diagnostics;
using System.Security.Claims;

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
        private readonly IAuthorizationService _authorizationService;
        public BillController(IBillRepository billRepository, IItemRepository itemRepository, ISellerRepository seller, ISoldProductRepository soldProductRepository, IAuthorizationService authorizationService)
        {
            _billRepository = billRepository;
            _itemRepository = itemRepository;
            _sellerRepository = seller;
            _oldProductRepository = soldProductRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Alll)]
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
        [Authorize(Roles = Roles.Alll)]
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
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<BillDto>> Create(CreateBillDot createBillDot, int sellerId, int itemId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null) return NotFound();


            var bill = new saskaita
            { Vardas = createBillDot.BuyerName,
                Pavarde = createBillDot.BuyerSecondName,
                Miestas = createBillDot.city, Adresas = createBillDot.address,
                PirkimoData = createBillDot.DateTime,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            
            };
            Debug.WriteLine("fdg-uhkhj--------------------------------------------");
            Debug.WriteLine(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            await _billRepository.CreateAsync(bill);
            var sold = new perkamapreke { fk_PrekeId = itemId, fk_SaskaitaId = bill.Id};
            await _oldProductRepository.CreateAsync(sold);
            // 201
            return Created("", new BillDto(bill.Id, bill.Vardas, bill.Pavarde, bill.Miestas, bill.Adresas, bill.PirkimoData));
        }

        [HttpPut]
        [Route("{billId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<BillDto>> Update(int billId, UpdateBillDot updatebillDto,int sellerId, int itemId)
        {
            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null) return NotFound();


            var bill = await _billRepository.GetAsync(billId);
            var authr = await _authorizationService.AuthorizeAsync(User, bill, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }
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
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Remove(int billId, int sellerId, int itemId)
        {
            var bill = await _billRepository.GetAsync(billId);

            var seller = await _sellerRepository.GetAsync(sellerId);
            if (seller == null) return NotFound();
            var item = await _itemRepository.GetAsync(itemId, sellerId);
            if (item == null) return NotFound();

            var sold = await _oldProductRepository.GetAsyncByBill(billId);
            if (sold == null)
            {
                return NotFound();
            }
            var authr = await _authorizationService.AuthorizeAsync(User, bill, PolicyNames.ResourceOwner);
            if (!authr.Succeeded)
            {
                return Forbid();
            }
            await _oldProductRepository.DeleteAsync(sold);

            // 404
            if (bill == null)
                return NotFound();

            await _billRepository.DeleteAsync(bill);


            // 204
            return NoContent();
        }
    }
}