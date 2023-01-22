using Report_BL.DataCollection;

namespace Report_BL.Controller.Tables
{
    public static class IsYearCreated
    {
        // проверяем если текущий год в таблице прибыли
        public static bool Get( int year_)
        {
            bool rez = false;
            foreach(var row in ProfitTableCollection.profitTable)
            {
                if(Int32.Parse(row.YearVal) == year_)
                {
                    rez = true;
                    break;
                }
            }
            return rez;
        }
    } 

}