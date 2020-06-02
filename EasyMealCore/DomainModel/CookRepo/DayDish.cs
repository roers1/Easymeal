namespace EasyMealCore.DomainModel.CookRepo
{
	//koppelobject
	public class DayDish
	{
		public int DayId { get; set; }
		public Day Day { get; set; }

		public int DishId { get; set; }
		public Dish Dish { get; set; }
	}
}
