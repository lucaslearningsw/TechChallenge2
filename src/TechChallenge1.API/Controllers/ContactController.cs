using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using TechChallenge1.Core.DomainExceptions;
using TechChallenge1.Core.DTO;

using TechChallenge1.Domain.Interfaces;
using TechChallenge1.Domain.Models;


namespace TechChallenge1.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IStateService _stateService;
        
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper, IStateService stateService)
        {
            _mapper = mapper;
            _contactService = contactService;
            _stateService = stateService;
        }

        [HttpGet]
        [Route("list-contact")]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
               return Ok(_mapper.Map<IEnumerable<ContactDto>>(await _contactService.GetAll()));
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        [HttpGet]
        [Route("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetContactById([FromRoute] Guid id)
        {
            try
            {
                var contact = await _contactService.GetById(id);

                if (contact is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ContactDto>(await _contactService.GetById(id)));
            }
            catch (Exception)
            {
                throw;
            }

        }


        [Route("register-contact")]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactDto contact)
        {
            try
            {
                contact = await FillState(contact);
                await _contactService.Create(_mapper.Map<Contact>(contact));

                return Ok(contact);
            }
            catch (DomainException e)
            {
                return (BadRequest(new {e.Message}));
            }
        }

        [HttpDelete]
        [Route("delete-contact/{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await _contactService.GetById(id);

            if (contact is null)
            {
                return NotFound();
            }

            await _contactService.Delete(id);

            return Ok();
        }

        [HttpPatch]
        [Route("update-contact")]
        public async Task<IActionResult> UpdateUser([FromBody] ContactDto dto)
        {
            //TODO: fluent validion
            var contact = await _contactService.GetById(dto.Id);

            if (contact is null)
            {
                return NotFound();
            }
            try
            {
                dto = await FillState(dto);

                var entity = _mapper.Map<Contact>(dto);
                await _contactService.Update(entity);

            }
            catch (Exception e)
            {
                return (BadRequest(new { Message = e.Message }));
            }

            return Ok(dto);
        }


        private async Task<ContactDto> FillState(ContactDto contact)
        {
            contact.State =  _mapper.Map<StateDto>(await _stateService.GetByDDD(contact.State.DDD));

            return contact;
        }
    }
}
