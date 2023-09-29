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

        /// <summary>
        /// Calculated by adding elapsedTime to the start date
        /// </summary>
        public DateTime endDate 
        {
            get
            {
                return startDate.AddDays(elapsedTime);
            }
        }

        /// <summary>
        /// Claculated by adding the allottedTime to the startDate
        /// </summary>
        public DateTime dueDate 
        {
            get
            {
                return startDate.AddDays(allottedTime);
            }
        }

        /// <summary>
        /// Calculted by substrating allottedTime from the elapsedTime
        /// if the status is false.
        /// </summary>
        public int daysOverDue 
        { 
            get
            {
                return !status ? 
                    elapsedTime - allottedTime : 
                    0;
            } 
        }

        /// <summary>
        /// Calculated by substracting elapsedTime from the
        /// allottedTime if the status is true
        /// </summary>
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
