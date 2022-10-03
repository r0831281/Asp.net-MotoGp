using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
	public class ShopViewModel
	{
		public List<Race> Races { get; set; }
		public SelectList Countries { get; set; }
		public Ticket Ticket { get; set; }
	}
}
