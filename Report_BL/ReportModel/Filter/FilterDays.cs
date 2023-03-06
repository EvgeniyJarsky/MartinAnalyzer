namespace Report_BL.ReportModel
{
    public class FilterDays
    {
        public bool Monday {get; set;} = true;
        public bool Tuesday  {get; set;} = true;
        public bool Wednesday  {get; set;} = true;
        public bool Thursday {get; set;} = true;
        public bool Friday  {get; set;} = true;
        
        

        public bool[] daysFilterMassive()
        {
            return new bool[7]
            {
                false,
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday,
                false
            };
        }  

    }
}