//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace course_project_v0._0._2.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public string ticketID { get; set; }
        public string sessionID { get; set; }
        public string userID { get; set; }
        public int price { get; set; }
    
        public virtual Session Session { get; set; }
        public virtual UsersBD UsersBD { get; set; }
    }
}
