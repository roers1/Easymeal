namespace EasyMealCore.DomainModel.CookRepo
{
	public class WeekplanningDay
	{
		public int WeekplanningId { get; set; }
		public Weekplanning Weekplanning { get; set; }

		public int DayId { get; set; }
		public Day Day { get; set; }
	}
}
