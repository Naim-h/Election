﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Election.Models
{
    public partial class PersonElection
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ElectionId { get; set; }
        public bool IsPositionElected { get; set; }

        public virtual ElectionInfo Election { get; set; }
        public virtual Person Person { get; set; }
    }
}