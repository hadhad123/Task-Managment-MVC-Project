using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IEntityBase //All entities will have an ID property mapping their primary key in the database
    {
        int ID { get; set; }
    }
}
