using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactNoteController : ControllerBase
    {

        IContactNoteService contactNoteService;
        public ContactNoteController(IContactNoteService contactNoteService)
        {
            this.contactNoteService = contactNoteService;
        }



        //[HttpPost]
        //[Route("SearchContractNote")]
        //public ContactNoteRes SearchContractNote()
        //{
        //    var res = new ContactNoteRes();
        //    try
        //    {
        //        ContactNoteReq req = new ContactNoteReq();
        //        res = contactNoteService.Search(req);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.status_code = "500";
        //        res.status_text = ex.Message;
        //        res.payload = null;
        //    }
        //    return res;
        //}
    }
}
