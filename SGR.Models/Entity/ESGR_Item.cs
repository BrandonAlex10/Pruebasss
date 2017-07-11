/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN HERNANDEZ VILLO
'* FCH. CREACIÓN : 16/12/2016
**********************************************************/
namespace SGR.Models.Entity
{
    public class ESGR_Item
    {
        public ESGR_Item() 
        {
            ValueMemberPath = string.Empty;
            ValueValuePath = string.Empty;
        }

        public string ValueMemberPath { get; set; }
        public string ValueValuePath { get; set; }
    }
}
