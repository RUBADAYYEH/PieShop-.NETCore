using PieShop.Models;

namespace PieShop.ViewModels
{
	public class PieListViewModel
	{
		public string? CurrentCategory { get;} 
		public IEnumerable<Pie> Pies {  get;}
		public PieListViewModel(IEnumerable<Pie> pies,string? currentCategory)
		{
			Pies = pies;
			CurrentCategory = currentCategory;
		}
	}
}
