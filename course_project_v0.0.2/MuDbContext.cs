
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace course_project_v0._0._2
{
	public class MuDbContext : DbContext
	{
		protected MuDbContext() : base("DbConnectionString")
		{
		}
	}
}
