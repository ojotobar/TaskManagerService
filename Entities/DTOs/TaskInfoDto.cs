namespace Entities.DTOs
{
    public record TaskInfoDto
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public int allottedTime { get; set; }
        public int elapsedTime { get; set; }
        public bool status { get; set; }
        public DateTime endDate 
        {
            get
            {
                return startDate.AddDays(elapsedTime);
            }
        }
        public DateTime dueDate 
        {
            get
            {
                return startDate.AddDays(allottedTime);
            }
        }
        public int daysOverDue 
        { 
            get
            {
                return !status ? 
                    elapsedTime - allottedTime : 
                    0;
            } 
        }
        public int daysLate 
        {
            get
            {
                return status ? 
                    allottedTime - elapsedTime : 
                    0;
            } 
        }
    }
}
