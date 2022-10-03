using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MotoGP.Controllers
{
    public class ShopController :Controller
    {
        private readonly GPContext _context;

        public ShopController(GPContext context)
        {
            _context = context;
        }


        //get order ticket
        public IActionResult OrderTicket()
        {
            var shopView = new ShopViewModel();

            int BannerNr = 3;
            var races = _context.Races.OrderBy(r => r.Name).ToList();
            shopView.Races = races.ToList();
            shopView.Countries = new SelectList(_context.Countries.OrderBy(r => r.Name), "CountryID", "Name");
            ViewData["BannerNr"] = BannerNr;
            return View(shopView);
        }


        //post order ticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderTicket(Ticket ticket)
        {
            var shopView = new ShopViewModel();
            var races = _context.Races.OrderBy(r => r.Name).ToList();
            shopView.Races = races.ToList();
            shopView.Countries = new SelectList(_context.Countries.OrderBy(r => r.Name), "CountryID", "Name");
            ticket.OrderDate = DateTime.Now;
            ticket.Paid = false;
            int BannerNr = 3;
            ViewData["BannerNr"] = BannerNr;
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder", new{id = ticket.TicketID});

            }
            return View(shopView);
            
        }
        public IActionResult ConfirmOrder(int id)
        {
            int BannerNr = 3;
            ViewData["BannerNr"] = BannerNr;
            var ticket = _context.Tickets.Include(t => t.Race).SingleOrDefault(t => t.TicketID == id);
            return View(ticket);
        }
    }
}
