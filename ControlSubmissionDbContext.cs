using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi
{
    public class ControlSubmissionDbContext:DbContext
    {
        public ControlSubmissionDbContext():base("ConnectionString")
        {

        }
        public virtual DbSet<MileStone> MileStones { get; set; }

        public virtual DbSet<Recepient> Recepients { get; set; }

        public virtual DbSet<InformationRequest> InformationRequest { get; set; }
    }
}