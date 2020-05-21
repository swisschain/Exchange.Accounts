using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Services;
using Accounts.WebApi.Models.Wallet;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;
using Swisschain.Sdk.Server.WebApi.Common;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public WalletController(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Paginated<WalletModel, long>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionaryErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManyAsync([FromQuery] WalletRequestManyModel request)
        {
            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = User.GetTenantId();

            var wallets = await _walletService.GetAllAsync(brokerId, request.AccountId, request.Name,
                (Domain.Entities.Enums.WalletType)request.Type, request.IsEnabled, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<WalletModel[]>(wallets);

            return Ok(result.Paginate(request, Url, x => x.Id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WalletModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var brokerId = User.GetTenantId();

            var wallet = await _walletService.GetByIdAsync(id, brokerId);

            if (wallet == null)
                return NotFound();

            var model = _mapper.Map<WalletModel>(wallet);

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WalletModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(WalletAddModel wallet)
        {
            var domain = _mapper.Map<Wallet>(wallet);

            domain.BrokerId = User.GetTenantId();

            var newDomain = await _walletService.AddAsync(domain);

            var newViewModel = _mapper.Map<WalletModel>(newDomain);

            return Ok(newViewModel);
        }

        [HttpPut]
        [ProducesResponseType(typeof(WalletModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] WalletEditModel model)
        {
            var domain = _mapper.Map<Wallet>(model);

            domain.BrokerId = User.GetTenantId();

            var updated = await _walletService.UpdateAsync(domain);

            if (updated == null)
                return NotFound();

            var newModel = _mapper.Map<WalletModel>(updated);

            return Ok(newModel);
        }
    }
}
