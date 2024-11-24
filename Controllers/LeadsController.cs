using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeadManagementAPI.Data;
using LeadManagementAPI.Models;

namespace LeadManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController:ControllerBase
    {
        private readonly LeadContext _context;

        public LeadsController(LeadContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeads(string status = "Novo")
        {
            return await _context.Leads.Where(l => l.Status==status).ToListAsync();
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if(lead==null) return NotFound();

            if(lead.Price>500) lead.Price*=0.9m; // Apply 10% discount
            lead.Status="Aceito";

            // Fake email notification (optional)
            System.IO.File.WriteAllText("fake_email.txt",$"Email to sales@test.com: Lead {id} accepted.");

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> DeclineLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if(lead==null) return NotFound();

            lead.Status="Recusado";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
